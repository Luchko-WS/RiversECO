using RiversECO.Models;
using System.Threading.Tasks;

namespace RiversECO.Contracts.Repositories
{
    public interface ICriteriasRepository: IDataRepository<Criteria>
    {
        Task<Criteria> GetCriteriaByName(string name);
    }
}
