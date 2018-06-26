using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using _03LinqToSQL.Model;

namespace _03LinqToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------查询所有信息--------------");
            MyContext context = new MyContext();
            {

                var custonmer = context.Customers.Where(n => n.Id > 0).ToList();
                foreach (var custom in custonmer)
                {
                    Console.WriteLine($"id:{custom.Id}   name:{custom.Name}");
                }
            }
            Console.WriteLine("------------查询包含a信息的名字---------------");
            {
                IQueryable<string> query = from n in context.Customers
                                           where n.Name.Contains("a")
                                           orderby n.Name.Length
                                           select n.Name.ToLower();
                foreach (var name in query)
                {
                    Console.WriteLine($"name:{name}");
                }
            }

            Console.WriteLine("-------------简单查询------------------");
            {
                var count = context.Customers.Count();
                Customer cust = context.Customers.SingleOrDefault(n => n.Id == 1);
                Console.WriteLine($"总条数：{count}  Id:{cust.Id}  Name:{cust.Name}");

                Customer customer = context.Customers.First();
                customer.Name = "大海";
                context.SaveChanges(); //将第一天数据改变并保存到数据库
                var cc = context.Customers.ToList();
                foreach (var cu in cc)
                {
                    Console.WriteLine($"ID:{cu.Id}  Name:{cu.Name}");
                }
            }

            {
                Console.WriteLine("查询之前两行");
                var cus = context.Customers.OrderBy(x => x.Id).Take(2);
                foreach (var customer in cus)
                {
                    Console.WriteLine($"id:{customer.Id}   name:{customer.Name}");
                }

                char[] distinctLetters = "HelloWorld".Distinct().ToArray(); //Distinct 去重复
                Console.WriteLine("去重复后：" + new string(distinctLetters));
            }

            Console.WriteLine("-------------------子查询-----------------");
#if false

            {
                var query =
                              from c in dataContext.Customers
                              where c.Purchases.Any(p => p.Price > 1000)
                              select new
                              {
                                  c.Name,
                                  Purchases = from p in c.Purchases
                                              where p.Price > 1000
                                              select new { p.Description, p.Price }
                              };
            }
    //下面写法更简洁
            {
                var query =
                    from c in dataContext.Customers
                    let highValueP = from p in c.Purchases
                                     where p.Price > 1000
                                     select new { p.Description, p.Price }
                    where highValueP.Any()
                    select new { c.Name, Purchases = highValueP };
            }
    //数据转换
    {
  IQueryable<CustomerEntity> query =
                from c in dataContext.Customers
                select new CustomerEntity
                {
                    Name = c.Name,
                    Purchases = (from p in c.Purchases
                                 where p.Price > 1000
                                 select new PurchaseEntity {
                                     Description = p.Description,
                                     Value = p.Price
                                 }
                                 ).ToList()
                 };
 
            // 要强制执行query，可以把结果转换到普通List
            List<CustomerEntity> result = query.ToList();

}
#endif

            Console.WriteLine("-------------_____select 与 SelectMany 区别______----------");
            {
                string[] fullNames = { "Anne Williams", "Jhon Fred Smith", "Sue Green" };
                IEnumerable<string[]> query = fullNames.Select(name => name.Split());
                foreach (var names in query)
                    foreach (var name in names)
                        Console.Write(name + "|");
                Console.WriteLine();
                IEnumerable<string> query2 = fullNames.SelectMany(name => name.Split());
                foreach (var VARIABLE in query2)
                {
                    Console.Write(VARIABLE + "|");
                }

                //表达式方式
                IEnumerable<string> query3 = from fullName in fullNames
                                             from name in fullName.Split() // Translates to SelectMany
                                             select name;

                //表达式与SelectMany外部引用
                IEnumerable<string> query4 =
                    from fullName in fullNames // fullName = outer variable
                    from name in fullName.Split() // name = range variable
                    select name + " came from " + fullName; //外部引用
                IEnumerable<string> query5 = fullNames
                    .SelectMany(fName => fName.Split().Select(name => new { name, fName }))
                    .Select(x => x.name + " came from " + x.fName);


            }
            Console.WriteLine();
            Console.WriteLine("----------Cross Join--------------");
            {
                var query = from c in context.Customers
                            from i in context.CustomerInfos
                            where c.Id == i.Cid
                            select new { c.Id, c.Name, i.Address, i.Price };
                foreach (var cust in query)
                {
                    Console.WriteLine($"{cust.Id} {cust.Name}  {cust.Address} {cust.Price}");
                }

                Console.WriteLine("\r\n\r\n");
                // 无条件限制 Cross join
                var query2 = from c in context.Customers
                             where c.Name.StartsWith("T")
                             from i in context.CustomerInfos
                             select new { c.Name, i.Address };
                foreach (var c in query2)
                {
                    Console.WriteLine(c.Name);
                }

                Console.WriteLine("--------------导航查询--------------------");
                var query3 = from c in context.Customers
                             select new { c.Name, cInfo = c.CustomerInfos.FirstOrDefault(x => x.Address.Length > 0).Address };
                foreach (var c in query3)
                {
                    Console.WriteLine($"Name:{c.Name}  Info:{c.cInfo}");
                }

                Console.WriteLine("--------------left outer join -------------");
                var query4 = from c in context.Customers
                             from p in context.CustomerInfos.Where(p => Convert.ToInt32(p.Price) > 10).DefaultIfEmpty()
                             select new
                             {
                                 c.Name,
                                 Address = p == null ? null : p.Address,
                                 Price = p == null ? null : p.Price
                             };

                foreach (var c in query4)
                {
                    Console.WriteLine($"name:{c.Name} Address:{c.Address} Price:{c.Price}");
                }
            }

            Console.WriteLine("------------join-------------");
            {
                var fastQuery = from c in context.Customers
                                join i in context.CustomerInfos on c.Id equals i.Cid //join 可以一直连接下去
                                select c.Name + "    " + i.Address;
                foreach (var c in fastQuery)
                {
                    Console.WriteLine(c);
                }
                //多key匹配的join
                //from x in sequenceX
                //    join y in sequenceY on new { K1 = x.Prop1, K2 = x.Prop2 }
                //        equals new { K1 = y.Prop3, K2 = y.Prop4 }
                //            ...

                //join方法查询
                var methosQuery = context.Customers.Join(context.CustomerInfos, c => c.Id, i => i.Cid,
                    (c, i) => new { c.Name, i.Address, i.Price });
                foreach (var c in methosQuery)
                {
                    Console.WriteLine($"Name:{c.Name}  Address:{c.Address}  Price:{c.Price}");
                }

                //join并排序
                context.Customers.Join( // outer collection
                        context.CustomerInfos, // inner collection
                        c => c.Id, // outer key selector
                        p => p.Cid, // inner key selector
                        (c, p) => new { c, p }) // result selector
                    .OrderBy(x => x.p.Price)
                    .Select(x => x.c.Name + " bought a " + x.p.Address);
            }
            Console.WriteLine("----------Group join------------");

            {
                var query = from c in context.Customers
                            join i in context.CustomerInfos on c.Id equals i.Cid //可以加上条件   对于本地查询来说  Join比select 效率高
                                into custCustInfo
                            select new { CustName = c.Name, custCustInfo };

                foreach (var c in query)
                {
                    Console.WriteLine(c.CustName);
                    foreach (var cc in c.custCustInfo)
                    {
                        Console.WriteLine($"id:{cc.Id} Address:{cc.Address} Cid:{cc.Cid} price:{cc.Price}");
                    }
                }
            }
            Console.WriteLine("---------------分组与排序()------------------");
            {
                string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
                IEnumerable<string> query = names.OrderBy(x => x.Length).ThenBy(x => x); //先按照长度 然后按照字母排序
                foreach (var name in query)
                {
                    Console.Write(name + "|");
                }

                // 先按长度排序，然后按第二个字符排序，再按第一个字符排序
                IEnumerable<string> query2 =
                    names.OrderBy(s => s.Length).ThenBy(s => s[1]).ThenBy(s => s[0]);

                // 对应的查询表达式语法为
                IEnumerable<string> query3 =
                    from s in names
                    orderby s.Length, s[1], s[0]
                    select s;

#if false //降序
                var query = dataContext.Purchases.OrderByDescending(p => p.Price)
                                      .ThenBy(p => p.Description);

                // 查询表达式语法
                var query = from p in dataContext.Purchases
                            orderby p.Price descending, p.Description
                            select p;


    //group by 
       files.GroupBy(file => Path.GetExtension(file), file => file.ToUpper())
                     .OrderBy(grouping => grouping.Key);

                 var query =
                from file in files
                group file.ToUpper() by Path.GetExtension(file);

    //  和select一样，group也会结束一个查询，除非我们增加了一个可以继续查询的子句(into 关键字)：

            var query =
                from file in files
                group file.ToUpper() by Path.GetExtension(file) into grouping
                orderby grouping.Key
                select grouping;
续写查询对于group by运算符来说非常有用，因为我们很可能要对分组进行过滤等操作。 group by之后的where子句相当于SQL中的HAVING

            // 只选择元素数量小于3的分组
            var query =
                from file in files
                group file.ToUpper() by Path.GetExtension(file) into grouping
                where grouping.Count() < 3
                select grouping;

    //多条件分组
        var query = from p in dataContext.Purchases
                        group p by new { Year = p.Date.Year, Month = p.Date.Month };
#endif
            }
            Console.WriteLine("-------------运算符(First  FirstOrDefault,single  last,ElemettAt DefaultIfEmpty )------------");

            {
                int[] numbers = { 1, 2, 3, 4, 5 };
                int first = numbers.First(); // 1
                int last = numbers.Last(); // 5
                int firstEven = numbers.First(n => n % 2 == 0); // 2
                int lastEven = numbers.Last(n => n % 2 == 0); //4

                //   int firstBigError = numbers.First(n => n > 10); // Exception 找不到因此异常
                int firstBigNumber = numbers.FirstOrDefault(n => n > 10); // 0

                int onlyDivBy3 = numbers.Single(n => n % 3 == 0); // 3
                //   int divBy2Err = numbers.Single(n => n % 2 == 0);            // Error: 2 & 4 match
                //   int singleError = numbers.Single(n => n > 10);              // Error
                int noMatches = numbers.SingleOrDefault(n => n > 10); // 0
                //   int divBy2Error = numbers.SingleOrDefault(n => n % 2 == 0); // Error 有一个或者0个匹配 这里是2个

                //----------集合方法 ---- Count, LongCount  Min, Max Sum, Average  Aggregate(执行定制的计算)---略
                int digitCount = "pa55w0rd".Count(c => char.IsDigit(c)); //有三个数字
                //-----------量词---

                int[] numbers1 = { 2, 3, 4 };
                int[] numbers2 = { 2, 3, 4 };
                int[] numbers3 = { 3, 2, 4 };
                //SequenceEqual比较两个sequence。如果他们拥有相同的元素，且相同的顺序，则返回true：
                Console.WriteLine(numbers1.SequenceEqual(numbers2)); // True
                Console.WriteLine(numbers1.SequenceEqual(numbers3)); // False}
            }

            Console.WriteLine("-----------------操作运算符----------------");
            {
                Console.WriteLine("-----------------集合运算符-----------------");
                Console.WriteLine();
                //Concat 连接两个sequences的所有元素 对应sql UNION ALL
                //Union   连接两个sequences的所有元素，但去除重复的元素  UNION
                //Intersect  返回在两个sequence中都存在的元素  WHERE...IN(...)
                //Except 返回存在于第一个sequence而不存在于第二个sequence中的元素  EXCEPT  or  WHERE...NOT IN(...)
                int[] seq1 = { 1, 2, 3, 4 }, seq2 = { 3, 4, 5, 6 };
                IEnumerable<int> union = seq1.Union(seq2);
                IEnumerable<int> concat = seq1.Concat(seq2);
                foreach (var i in union) Console.Write(i + ",");
                Console.WriteLine();
                foreach (var i in concat) Console.Write(i + ",");
                IEnumerable<int>
                commonality = seq1.Intersect(seq2), // { 3,4}
                difference1 = seq1.Except(seq2), // { 1, 2 }
                difference2 = seq2.Except(seq1); // { 5,6 }
                Console.WriteLine();

                int[] numbers = { 3, 5, 7 };
                string[] words = { "three", "five", "seven", "ignored" };
                IEnumerable<string> zip = numbers.Zip(words, (n, w) => n + "=" + w); //取词匹配
                foreach (var str in zip)
                {
                    Console.Write($"{str}|");
                }
                Console.WriteLine();
                Console.WriteLine("----------------转换操作------------------");
                //OfType 把IEnumerable转换到IEnumerable<T>，丢弃错误的类型元素
                //Cast   把IEnumerable转换到IEnumerable<T>，如果存在错误的类型元素则抛出异常
                //ToArray   把IEnumerable<T> 转换到T[]
                //ToList 把IEnumerable< T > 转换到List < T >
                //ToDictionary  把IEnumerable<T> 转换到Dictionary<TKey, TValue>
                //ToLookup 把IEnumerable< T > 转换到ILookup < TKey, TElement >
                //AsEnumerable  向下转换到IEnumerable<T>
                //AsQueryable   转换到IQueryable<T>
                ArrayList arr = new ArrayList();
                arr.AddRange(new int[] { 3, 4, 5 });
                arr.Add(new DateTime());
                IEnumerable<int> seq = arr.OfType<int>();
                IEnumerable<long> seqq = arr.OfType<long>(); //int 与long没有继承关系 转换后seqq结果为0
                IEnumerable<string> seqqq = arr.OfType<string>(); //int 与string没有继承关系 转换后seqq结果为0
                IEnumerable<int> se = arr.Cast<int>();//出现非int的数据类型 在遍历的时候抛出异常
                foreach (var i in seqqq)
                {
                    Console.WriteLine(i);
                }
                /*
                 * Range和Repeat只能用来操作integers。Range接受一个起始索引和元素个数：

            foreach (int i in Enumerable.Range(5, 5))
                Console.Write(i + ""); // 5 6 7 8 9
           Repeat接受重复的数值，和该数值重复的次数：

            foreach (int i in Enumerable.Repeat(5, 3))
                Console.Write(i + ""); // 5 5 5
                 */
                int[][] numberss =
                {
                    new int[] { 1, 2, 3 },
                    new int[] { 4, 5, 6 },
                    null // this null makes the query below fail.
                };
                IEnumerable<int> flat = numberss
                    .SelectMany(innerArray => innerArray ?? Enumerable.Empty<int>());
                foreach (int i in flat)
                    Console.Write(i + " "); // 1 2 3 4 5 6

            }

            Console.ReadKey();

        }


    }
}
