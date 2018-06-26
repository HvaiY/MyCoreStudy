using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Linq;
using Database;

/// <summary> 
///linqHelper 的摘要说明 
/// </summary> 
namespace Business
{
    public abstract class linqHelper<TDatabase>
where TDatabase : DataContext, new()
    {
        /// <summary> 
        /// 查询全部数据 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <returns></returns> 
        public static List<T> ReturnAllRows<T>(string conString) where T : class
        {
            TDatabase database = new TDatabase();

            database.Connection.ConnectionString = conString;
            return database.GetTable<T>().ToList<T>();
        }
        /// <summary> 
        /// 查看是否存在数据 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="predicate"></param> 
        /// <returns></returns> 
        public static bool EntityExists<T>(string conString, Expression<Func<T, bool>> predicate)
        where T : class
        {

            TDatabase database = new TDatabase();

            database.Connection.ConnectionString = conString;
            return database.GetTable<T>().Where<T>(predicate).Count() > 0;
        }

        /// <summary> 
        /// 有条件的查询数据List<typeparamref name="数据源:DataContext"/>  Filter<typeparamref name="表的类名:orders"/>  
        /// 最后面的可以输入LINQ查询语句（p=>p.order_id="00001")或者(form o in orders where o.order_id>100 sleect o); 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="predicate"></param> 
        /// <returns></returns> 
        public static List<T> Filter<T>(string conString, Expression<Func<T, bool>> predicate)
        where T : class
        {
            TDatabase database = new TDatabase();

            database.Connection.ConnectionString = conString;
            return database.GetTable<T>().Where(predicate).ToList<T>();
        }

        /// <summary> 
        /// 插入数据,好象只能一条,测试完弄个数据插入多条 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="entity"></param> 
        public static void Insert<T>(string conString, T entity) where T : class
        {
            using (TDatabase database = new TDatabase())
            {
                database.Connection.ConnectionString = conString;
                database.GetTable<T>().InsertOnSubmit(entity);
                database.SubmitChanges();
            }
        }

        /// <summary> 
        /// 删除指定数据,支持多条删除  Expression<Func<T, bool>> predicate就是查询语句，只能用：p=>p.user_id=="123"的语句！ 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="predicate"></param> 
        ///返回值：0 成功    -1 失败 
        public static int Delete<T>(string conString, Expression<Func<T, bool>> predicate)
        where T : class
        {
            if (EntityExists<T>(conString, predicate))
            {
                using (TDatabase database = new TDatabase())
                {
                    database.Connection.ConnectionString = conString;
                    T t = (T)database.GetTable<T>().Where<T>(predicate).Single();
                    database.GetTable<T>().DeleteOnSubmit(t);
                    database.SubmitChanges();
                }
                return 0;
            }
            return -1;
        }

        /// <summary> 
        /// 返回分页面数据 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="pageSize">一次取多少条数据</param> 
        /// <param name="currerCount">当前提交的页数字</param> 
        /// <returns></returns> 
        public static List<T> getpPgeRow<T>(string conString, int pageSize, int currerCount) where T : class
        {
            TDatabase database = new TDatabase();

            database.Connection.ConnectionString = conString;
            return database.GetTable<T>().Skip<T>((currerCount - 1) * pageSize).Take<T>(pageSize).ToList<T>();
        }

        /// <summary> 
        /// 返回多少条数据 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="predicate"></param> 
        /// <returns></returns> 
        public static int getPageCount<T>(string conString, Expression<Func<T, bool>> predicate) where T : class
        {
            TDatabase database = new TDatabase();

            database.Connection.ConnectionString = conString;
            return database.GetTable<T>().Where<T>(predicate).Count();
        }

        /// <summary> 
        /// 一次插入多条数据  
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="entity"></param> 
        public static void insertRows<T>(string conString, List<T> entity) where T : class
        {
            using (TDatabase database = new TDatabase())
            {
                database.Connection.ConnectionString = conString;
                foreach (T t in entity.ToList())
                {
                    database.GetTable<T>().InsertOnSubmit(t);
                    database.SubmitChanges();
                }

            }
        }
    }

}
