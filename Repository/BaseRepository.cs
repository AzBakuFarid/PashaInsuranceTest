using Microsoft.EntityFrameworkCore;
using PashaInsuranceTest.Data;
using PashaInsuranceTest.DbEntities.Interfaces;
using PashaInsuranceTest.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PashaInsuranceTest.Repository
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public virtual void Create<TDbEntity>(TDbEntity model) where TDbEntity : class
        {
            if (model != null)
            {
                _dbContext.Set<TDbEntity>().Add(model);
            }
        }
        public virtual void Update<TDbEntity>(TDbEntity model) where TDbEntity : class
        {
            var entity = _dbContext.Set<TDbEntity>().Attach(model);
            entity.State = EntityState.Modified;
        }

        public virtual List<TDbEntity> List<TDbEntity>(params string[] relations) where TDbEntity : class
        {
            var dbSet = _dbContext.Set<TDbEntity>();
            var query = ((IQueryable<TDbEntity>)dbSet).AddInclude(relations);

            return query.ToList();
        }
        public virtual TDbEntity Find<TDbEntity, TKeyType>(TKeyType id, params string[] relations) where TDbEntity : class, IKey<TKeyType>
        {
            var dbSet = _dbContext.Set<TDbEntity>();
            var query = ((IQueryable<TDbEntity>)dbSet).AddInclude(relations);

            return query.FirstOrDefault(f => f.Id.Equals(id));
        }
        public virtual void Delete<TDbEntity>(TDbEntity entity) where TDbEntity : class
        {
            _dbContext.Remove(entity);
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public virtual TDbEntity FindByName<TDbEntity>(string name, params string[] relations) where TDbEntity : class, IName
        {
            var dbSet = _dbContext.Set<TDbEntity>();
            var query = ((IQueryable<TDbEntity>)dbSet).AddInclude(relations);
            return query.FirstOrDefault(f => f.Name.Equals(name));
        }
        public virtual List<TDbEntity> ListByIds<TDbEntity, TKeyType>(IEnumerable<TKeyType> idList) where TDbEntity : class, IKey<TKeyType>
        {
            return _dbContext.Set<TDbEntity>().Where(w => idList.Contains(w.Id)).ToList();
        }

    }
    ///////////////////////////////////////////////////////////////////////////////////////////// 
    public interface IBaseRepository
    {
        void Create<TDbEntity>(TDbEntity model) where TDbEntity : class;
        void Update<TDbEntity>(TDbEntity model) where TDbEntity : class;
        TDbEntity Find<TDbEntity, TKeyType>(TKeyType id, params string[] relations) where TDbEntity : class, IKey<TKeyType>;
        List<TDbEntity> List<TDbEntity>(params string[] relations) where TDbEntity : class;
        TDbEntity FindByName<TDbEntity>(string name, params string[] relations) where TDbEntity : class, IName;
        List<TDbEntity> ListByIds<TDbEntity, TKeyType>(IEnumerable<TKeyType> idList) where TDbEntity : class, IKey<TKeyType>;
        void Delete<TDbEntity>(TDbEntity entity) where TDbEntity : class;
        void Commit();
    }
}
