using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class CriteriasRepository : DataRepository<Criteria>, ICriteriasRepository
    {
        public CriteriasRepository(DataContext.DataContext context)
            : base(context) { }

        public override async Task<PagedList<Criteria>> GetAllAsync()
        {
            var criterias = await _context.Criterias.ToListAsync();

            var pagedList = new PagedList<Criteria>
            {
                PageNumber = 1,
                PageSize = criterias.Count,
                Total = criterias.Count
            };
            pagedList.AddRange(criterias);

            return pagedList;
        }

        public override async Task<Criteria> GetByIdAsync(Guid id)
        {
            var criteria = await _context.Criterias
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return criteria;
        }
    }
}
