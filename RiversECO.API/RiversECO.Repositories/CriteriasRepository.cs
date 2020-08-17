using System.Linq;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class CriteriasRepository : DataRepository<Criteria>, ICriteriasRepository
    {

        public CriteriasRepository(DataContext.DataContext context)
            : base(context) { }

        public override IQueryable<Criteria> Items
        {
            get
            {
                return _context.Criterias.AsQueryable();
            }
        }
    }
}
