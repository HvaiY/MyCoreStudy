using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Text;

namespace _03LinqToSQL.Model
{

    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerInfo> CustomerInfos { get; set; }
    }

    public class CustomerInfo
    {
        public int Id { get; set; }
        public int Cid { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
        public Customer Customer { get; set; }
    }

    public class CustomerMore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CIid { get; set; }
    }

}
