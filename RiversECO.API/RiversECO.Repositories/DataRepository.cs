using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task<PagedList<TModel>> GetPagedAsync()
        {
            var itemsToReturn = await Items.ToListAsync();

            var pagedList = new PagedList<TModel>
            {
                PageNumber = 1,
                PageSize = itemsToReturn.Count,
                Total = itemsToReturn.Count
            };
            pagedList.AddRange(itemsToReturn);

            return pagedList;
        }

        public virtual async Task<TModel> GetByIdAsync(Guid id)
        {
            var item = await Items
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return item;
        }

        public virtual void Create(TModel model)
        {
            model.Id = Guid.NewGuid();
            model.CreatedOn = DateTime.UtcNow;

            _context.Add(model);
        }

        public virtual void Update(TModel model)
        {
            model.ModifiedOn = DateTime.UtcNow;

            _context.Update(model);
        }

        public virtual void Delete(Guid id)
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
