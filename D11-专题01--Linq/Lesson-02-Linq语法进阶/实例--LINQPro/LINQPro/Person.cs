using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQPro
{
    class Person
    {
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            string message = string.Empty;
            message = string.Format("{0} {1}: {2}", this.FirstName, this.LasName, this.Age);
            return message;
        }
    }
}
