using System;
using System.Threading.Tasks;
using RiversECO.Models;

namespace RiversECO.Contracts.Repositories
{
    public interface IDataRepository<TModel> where TModel: ModelBase
    {
        Task<TModel> GetByIdAsync(Guid id);
        Task<PagedList<TModel>> GetAllAsync();

        void Create(TModel model);
        void Update(TModel model);
        void Delete(Guid id);
        Task<bool> SaveAllChangesAsync();
    }
}
