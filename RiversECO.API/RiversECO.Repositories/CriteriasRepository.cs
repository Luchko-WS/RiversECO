using System;
using System.Collections.Generic;
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

        public override async Task<IList<Criteria>> GetAllAsync()
        {
            var criterias = await _context.Criterias.ToListAsync();
            return criterias;
        }

        public override async Task<Criteria> GetByIdAsync(Guid id)
        {
            var criteria = await _context.Criterias
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return criteria;
        }
    }
}
