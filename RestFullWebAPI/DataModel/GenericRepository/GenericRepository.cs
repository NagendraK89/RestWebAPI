using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.GenericRepository
{
    public class GenericRepository<T> where T : class
    {
        #region Private member variables
        private RestFullWebAPIDbEntities Context;
        private DbSet<T> Dbset;
        #endregion Private member variables

        #region Public Constructor
        public GenericRepository(RestFullWebAPIDbEntities context)
        {
            this.Context = context;
            this.Dbset = context.Set<T>();
        }
        #endregion Public Constructor
        #region Public member functions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> query = Dbset;
            return query.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual T GetById(object Id)
        {
            return Dbset.Find(Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(T entity)
        {
            Dbset.Add(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public virtual void Delete(object Id)
        {
            T entity = Dbset.Find(Id);
            Delete(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Dbset.Attach(entity);
            }
            Dbset.Remove(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            Dbset.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return Dbset.Where(where).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetManyQueryable(Func<T, bool> where)
        {
            return Dbset.Where(where).AsQueryable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Func<T, bool> where)
        {
            return Dbset.Where(where).FirstOrDefault<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Func<T, bool> where)
        {
            IQueryable<T> objects = Dbset.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                Dbset.Remove(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<T> GetWithInclude(
           System.Linq.Expressions.Expression<Func<T,
           bool>> predicate, params string[] include)
        {
            IQueryable<T> query = this.Dbset;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey)
        {
            return Dbset.Find(primaryKey) != null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetSingle(Func<T, bool> predicate)
        {
            return Dbset.Single<T>(predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetFirst(Func<T, bool> predicate)
        {
            return Dbset.First<T>(predicate);
        }

        #endregion Public member functions
    }
}
