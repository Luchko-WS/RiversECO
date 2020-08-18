using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiversECO.Models;

namespace RiversECO.Contracts.Repositories
{
    public interface IDataRepository<TModel> where TModel: ModelBase
    {
        IQueryable<TModel> Items { get; }

        Task<TModel> GetByIdAsync(Guid id);
        Task<IList<TModel>> GetAllAsync();
        Task<PagedList<TModel>> GetPagedAsync();

        void Create(TModel model);
        void Update(TModel model);
        void Delete(Guid id);
        Task<bool> SaveAllChangesAsync();
    }
}
