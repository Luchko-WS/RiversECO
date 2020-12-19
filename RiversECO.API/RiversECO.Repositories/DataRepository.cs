using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiversECO.Common.Exceptions;
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

        public abstract IQueryable<TModel> Items { get; }

        public virtual async Task<IList<TModel>> GetAllAsync()
        {
            var items = await Items.ToListAsync();
            return items;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            var item = await Items
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (item == null)
            {
                throw new DataNotFoundException($"{nameof(TModel)} with id {id} not found.");
            }

            return item;
        }

        public virtual void Create(TModel model)
        {
            model.Id = Guid.NewGuid();
            model.CreatedOn = DateTime.UtcNow;
            model.Name = model.Name.Trim();

            _context.Add(model);
        }

        public virtual void Update(TModel model)
        {
            model.ModifiedOn = DateTime.UtcNow;
            model.Name = model.Name.Trim();

            _context.Update(model);
        }

        public virtual void Delete(Guid id)
        {
            var item = GetByIdAsync(id).Result;
            if (item == null)
            {
                throw new DataNotFoundException($"{nameof(TModel)} with id {id} not found.");
            }

            _context.Remove(item);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
