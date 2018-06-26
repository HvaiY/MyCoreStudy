using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _02LinqMore
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 延迟执行
            //  除了两种情况都是延迟执行的 返回单个元素： First、Count等，或者转换运算符 ToArray() 、Tolist()、 Todictionary()、ToLookup();
            var numbers = new List<int> { 1, 2 };
            IEnumerable<int> Ilist = numbers.Select(n => n * 10);
            foreach (var VARIABLE in Ilist)
            {
                Console.WriteLine(VARIABLE);
            }
            numbers.Clear();
            Console.WriteLine("------清空后延迟执行效果--------");
            foreach (var num in Ilist)
            {
                Console.WriteLine(num);
            }

            var numbers2 = new List<int> { 1, 2 };
            //转为List后效果  Tolist()
            Console.WriteLine("转为集合后就没有延迟效果了");
            List<int> timesTen = numbers2
                .Select(n => n * 10)
                .ToList();   // Executes immediately into a List<int>

            numbers2.Clear();
            Console.Write(timesTen.Count);  // Still 2
            #endregion

            #region 延迟执行异常
            Console.WriteLine("-----------------------------");
            int[] nums = { 1, 2 };
            int factor = 10;
            IEnumerable<int> querys2 = nums.Select(n => n * factor);
            factor = 20; //存在延迟执行 所有当factor值改变的时候 那么也执行的条件会变 ，因此易出现异常就在这里了
            foreach (var n in querys2)
            {
                Console.WriteLine(n + "|");
            }
            #endregion
            Console.WriteLine("-----------------------------");
            #region 特别注意延迟执行的坑
            IEnumerable<char> qu = "How are you, friend.";

            foreach (char vowel in "aeiou")
                qu = qu.Where(c => c != vowel);
            foreach (char c in qu) Console.Write(c); //How are yo, friend.//Linq版本改进 这里结果是 Hw r y, frnd.

            //IEnumerable<char> query = "How are you, friend.";

            //query = query.Where(c => c != 'a');
            //query = query.Where(c => c != 'e');
            //query = query.Where(c => c != 'i');
            //query = query.Where(c => c != 'o');
            //query = query.Where(c => c != 'u');

            //foreach (char c in query) Console.Write(c); //Hw r y, frnd.
            #endregion
            Console.WriteLine("-----------------------------");
            #region 子查询
            {
                string[] names = { "David Tim", "Tony Sin", "Rager Witers" };
                IEnumerable<string> query = names.OrderBy(n => n.Split().Last());
                foreach (var name in query)
                {
                    Console.WriteLine(name);
                }
            }
            //子查询2
            {
                //子查询2
                string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
                // 获取所有长度最短的名字（注意：可能有多个）
                IEnumerable<string> outQuery = names
                    .Where(n => n.Length == names.OrderBy(n2 => n2.Length).Select(n2 => n2.Length).First());      // Tom, Jay"

                // 与上面方法语法等价的查询表达式
                IEnumerable<string> outQuery2 =
                    from n in names
                    where n.Length ==            // 感谢A_明~坚持的指正，这里应该为==

                          (from n2 in names orderby n2.Length select n2.Length).First()
                    select n;

                // 我们可以使用Min查询运算符来简化
                IEnumerable<string> outQuery3 =
                    from n in names
                    where n.Length == names.Min(n2 => n2.Length)
                    select n;

                foreach (var VARIABLE in outQuery)
                {
                    Console.WriteLine(VARIABLE);
                }

                foreach (var VARIABLE in outQuery2)
                {
                    Console.WriteLine(VARIABLE);
                }

                foreach (var VARIABLE in outQuery3)
                {
                    Console.WriteLine(VARIABLE);
                }
            }
            //子查询 三 渐进式创建查询
            {
                Console.WriteLine("--------------------");
                string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
                IEnumerable<string> query = names.Select(n =>
                        n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", ""))
                    .Where(n2 => n2.Length > 2)
                    .OrderBy(n => n);

            }
            {
                string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
                IEnumerable<string> query =
                    from n in names
                    select n.Replace("a", "").Replace("e", "").Replace("i", "")
                        .Replace("o", "").Replace("u", ""); //表达式查询中 遇见select意味着查询结束，所以下面只能再开一个查询 但是还有into关键字可以使用

                query = from n in query
                        where n.Length > 2
                        orderby n
                        select n;   // Result: Dck, Hrry, Mry
                foreach (var name in query)
                {
                    Console.WriteLine(name);
                }
            }

            //into关键字
            {
                string[] names = { "David Tim", "Tony Sin", "Rager Witers" };
                IEnumerable<string> query =
                    from n in names
                    select n.Replace("a", "").Replace("e", "").Replace("i", "")
                        .Replace("o", "").Replace("u", "")
                    into noVowel //into之后只有 noVowel可见 因此 只能into 后加变量了
                    where noVowel.Length > 2
                    orderby noVowel
                    select noVowel;   // Result: Dck, Hrry, Mry


                //---------包装查询
                // 用包装查询方式进行改写(Wrapping Queries)
                IEnumerable<string> query2 =
                    from n1 in
                    (
                        from n2 in names
                        select Regex.Replace(n2, "[aeiou]", "")
                    )
                    where n1.Length > 2
                    orderby n1
                    select n1;

                // 与上面等价的方法语法 方法查询 select  不意味着结束
                IEnumerable<string> query3 = names
                    .Select(n => Regex.Replace(n, "[aeiou]", ""))
                    .Where(n => n.Length > 2)
                    .OrderBy(n => n);
            }
            #endregion

            Console.WriteLine("--------------------------");
            //匿名类查询 (匿名类 渐进式查询)
            {
                string[] names = { "David Tim", "Tony Sin", "Rager Witers" };
                var intermediate = from n in names
                    select new
                    {
                        Original = n,
                        Vowelless = Regex.Replace(n, "[aeiou]", "")
                    };
                IEnumerable<string> query = from item in intermediate
                    where item.Vowelless.Length > 2
                    select item.Original;

                foreach (var nae in query)
                {
                    Console.WriteLine(nae);
                }
            }
            //let 关键字(表达式就可以用let替换select 后面依然可以是用n）
            {
                string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
                var query = from n in names
                    let Vowelless = Regex.Replace(n, "[aeiou]", "")
                    where Vowelless.Length > 2
                    select n;   //正是因为使用了let，此时n仍然可见
            }
            Console.ReadKey();
        }
    }
}
