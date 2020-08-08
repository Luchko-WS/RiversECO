using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public abstract class DataRepository<TModel> : IDataRepository<TModel>
        where TModel : ModelBase
    {
        protected readonly DataContext.DataContext _context;

        public DataRepository(DataContext.DataContext context)
        {
            _context = context;
        }

        public abstract Task<IList<TModel>> GetAllAsync();

        public abstract Task<TModel> GetByIdAsync(Guid id);

        public void Create(TModel model)
        {
            _context.Add(model);
        }

        public void Update(TModel model)
        {
            _context.Update(model);
        }

        public void Delete(Guid id)
        {
            var entityToRemove = GetByIdAsync(id).Result;
            if (entityToRemove != null)
            {
                _context.Remove(entityToRemove);
            }
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
