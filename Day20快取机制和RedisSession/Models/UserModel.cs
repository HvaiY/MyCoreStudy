using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day20快取机制和RedisSession.Models
{
    [Serializable]
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
