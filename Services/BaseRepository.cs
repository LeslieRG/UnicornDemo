using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UnicornDemo.Entities.Models;
using UnicornDemo.Services;

namespace UnicornDemo.Services
{
class BaseRepository<TEntity> : IRepository <TEntity> where TEntity : class
{

internal UnicornDemoContext context;
internal DbSet<TEntity> dbSet;

    public BaseRepository(UnicornDemoContext context)
    {
        this.context = context;
            this.dbSet = context.Set<TEntity>();
            
    }

    public void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public void Delete(object id)
    {
         TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
            

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public TEntity GetByID(object id)
    {
            return dbSet.Find(id);
            

        }

        // public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        // {
        //    return dbSet.SqlQuery(query, parameters).ToList();
        // }

        public void Insert(TEntity entity)
    {
         dbSet.Add(entity);
    }

    public void Update(TEntity entityToUpdate)
    {
         dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
}