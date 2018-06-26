using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day24EntityFrameworkCore.Models
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
