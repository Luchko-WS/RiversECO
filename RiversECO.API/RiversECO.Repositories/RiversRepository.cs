using System.Linq;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class RiversRepository : DataRepository<River>, IRiversRepository
    {

        public RiversRepository(DataContext.DataContext context)
            : base(context) { }

        public override IQueryable<River> Items
        {
            get
            {
                return _context.Rivers.AsQueryable();
            }
        }
    }
}
