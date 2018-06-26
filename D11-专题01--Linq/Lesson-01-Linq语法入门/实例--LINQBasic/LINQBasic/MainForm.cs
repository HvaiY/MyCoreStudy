using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LINQBasic
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLinqStory_Click(object sender, EventArgs e)
        {
            //WithoutLinq();
            WithLinq();
        }

        /// <summary>
        /// 传统查询方式
        /// </summary>
        private void WithoutLinq()
        {
            int[] numbers = new int[] { 6, 4, 3, 2, 9, 1, 7, 8, 5 };

            List<int> even = new List<int>();

            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                {
                    even.Add(number);
                }
            }

            even.Sort();
            even.Reverse();

            listBox1.Items.Add("传统查询方式");
            foreach (int number in even)
            {
                listBox1.Items.Add(number.ToString());

            }

        }

        /// <summary>
        /// LINQ 查询方式
        /// </summary>
        private void WithLinq()
        {
            int[] numbers = new int[] { 6, 4, 3, 2, 9, 1, 7, 8, 5 };

            var even = numbers
                .Where(pl => pl % 2 == 0)
                .Select(pl => pl)
                .OrderBy(pl => pl);

            this.listBox1.Items.Add("LINQ 查询方式");
            foreach (var item in even)
            {
                this.listBox1.Items.Add(item.ToString());
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 处理字符串的委托原型
        delegate string ProcessString(string input);
        delegate int Process2Numbers(int x, int y);
        delegate void ProcessSingleNumber(int x);

        // 一个实例方法 准备映射到上面的委托 ProcessString
        private string LowerIt(string input)
        {
            return input.ToLower();
        }

        private void btnDelegateTest_Click(object sender, EventArgs e)
        {
            List<string> foxRiver8 = new List<string>
            {
                "Michael",
                "Lincoln",
                "Sucre",
                "Abruzzi",
                "T-Bag",
                "C-Note",
                "Tweener",
                "Charles"
            };

            // 传统委托方式
            ProcessString p1 = new ProcessString(LowerIt);
            foreach (string name in foxRiver8)
            {
                listBox1.Items.Add(p1(name));
            }
            listBox1.Items.Add("----------------");

            // 匿名方法方式
            ProcessString p2 = delegate(string input)
            {
                return input.ToLower();
            };
            foreach (string name in foxRiver8)
            {
                listBox1.Items.Add(p2(name));
            }
            listBox1.Items.Add("----------------");

            // Lambda 表达式方式
            //ProcessString p = (string input) => { return input.ToLower(); };
            //ProcessString p = input => { return input.ToLower(); };
            //ProcessString p = (string input) => input.ToLower();

            ProcessString p3 = abc => abc.ToUpper();

            foreach (string name in foxRiver8)
            {
                listBox1.Items.Add(p3(name));
            }

        }

        private void btnLINQDataOpDemo_Click(object sender, EventArgs e)
        {
            List<string> foxRiver8 = new List<string>{
                "Michael",
                "Lincoln",
                "Sucre",
                "Lbruzzi",
                "T-Bags",
                "C-Note",
                "Tweener",
                "Charles"
            };

            //// 获取数据
            //var q1 = foxRiver8.Select(name => name.ToLower());
            //foreach (string item in q1)
            //{
            //    listBox1.Items.Add(item.ToLower());
            //}
            //listBox1.Items.Add("---------------------");

            //// 过滤数据
            //var q2 = foxRiver8
            //    .Where(name => name.EndsWith("s"))
            //    .Select(name => name.ToUpper());
            //foreach (string item in q2)
            //{
            //    listBox1.Items.Add(item);
            //}
            //listBox1.Items.Add("---------------------");


            // 排序数据
            var q3 = foxRiver8
                .Where(name => name.Length > 5)
                .Select(name => name.ToLower())
                .OrderBy(name => name.Substring(0, 1));
            List<string> list = q3.ToList<string>();
            foreach (var item in q3)
            {
                listBox1.Items.Add(item.ToString());
            }
            ///
            listBox1.Items.Add("----------------");

            // 分组数据
            var q4 = foxRiver8
                .Where(name => name.Length > 5)
                .Select(name => name.ToLower())
                .GroupBy(name => name.Substring(0, 1));

            listBox1.Items.Add(q4.Count().ToString());
            foreach (var group in q4)
            {
                listBox1.Items.Add("---" + group.Key + "---");
                foreach (var item in group)
                {
                    listBox1.Items.Add(item.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //numbers.OrderBy(n=>n);
            //var sorts = numbers.ToList<int>().OrderBy(n => n);
            var sorts = numbers.OrderByDescending(n => n);
            foreach (var item in sorts)
            {
                listBox1.Items.Add(item.ToString());
            }
        }
    }
}
