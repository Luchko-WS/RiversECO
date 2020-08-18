using System.Linq;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class LakesRepository : DataRepository<Lake>, ILakesRepository
    {

        public LakesRepository(DataContext.DataContext context)
            : base(context) { }

        public override IQueryable<Lake> Items
        {
            get
            {
                return _context.Lakes.AsQueryable();
            }
        }
    }
}
