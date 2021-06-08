using Microsoft.EntityFrameworkCore;
using PashaInsuranceTest.Data;
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
                _dbContext.SaveChanges();
            }
        }
        public virtual void Update<TDbEntity>(TDbEntity model) where TDbEntity : class
        {
            var entity = _dbContext.Set<TDbEntity>().Attach(model);
            entity.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public virtual List<TDbEntity> List<TDbEntity>() where TDbEntity : class
        {
            return _dbContext.Set<TDbEntity>().ToList();
        }
        public TDbEntity Find<TDbEntity, TId>(TId id) where TDbEntity : class
        {
            return (TDbEntity) _dbContext.Find(typeof(TDbEntity), id);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////// 
    public interface IBaseRepository
    {
        void Create<TDbEntity>(TDbEntity model) where TDbEntity : class;
        void Update<TDbEntity>(TDbEntity model) where TDbEntity : class;
        TDbEntity Find<TDbEntity, TId>(TId id) where TDbEntity : class;
        List<TDbEntity> List<TDbEntity>() where TDbEntity : class;
    }
}
