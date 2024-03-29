﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public override IQueryable<Criteria> Items
        {
            get
            {
                return _context.Criterias.AsQueryable();
            }
        }

        public override async Task<IList<Criteria>> GetAllAsync()
        {
            var result = await base.GetAllAsync();
            return result.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
        }

        public async Task<Criteria> GetCriteriaByName(string name)
        {
            var criteria = await Items
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.Trim().ToLower());
            return criteria;
        }
    }
}
