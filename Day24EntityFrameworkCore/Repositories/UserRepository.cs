using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Day24EntityFrameworkCore.Models;

namespace Day24EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 封装增删改查（EF 使用 Linq 查询）
    /// </summary>
    public class UserRepository : IRepository<UserModel, int>
    {
        private readonly MyContext _context;

        public UserRepository(MyContext context)
        {
            _context = context;
        }
        public int Create(UserModel entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }
        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            _context.SaveChanges();
        }
        public IEnumerable<UserModel> Find(Expression<Func<UserModel, bool>> expression)
        {
            return _context.Users.Where(expression);
        }
        public UserModel FindById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }
        public void Update(UserModel entity)
        {
            var fuser = _context.Users.Single(u => u.Id == entity.Id);
            _context.Entry(fuser).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
    }
}
