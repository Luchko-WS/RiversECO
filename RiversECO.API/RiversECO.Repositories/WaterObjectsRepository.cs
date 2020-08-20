using System;
using System.Collections.Generic;
using System.Linq;
using RiversECO.Cache.Contracts;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class WaterObjectsRepository : DataRepository<WaterObject>, IWaterObjectsRepository
    {
        private readonly IAppMemoryCache _cache;

        private IDictionary<Guid, WaterObject> _waterObjects;

        public WaterObjectsRepository(DataContext.DataContext context, IAppMemoryCache cache)
            : base(context)
        {
            _cache = cache;
            InitRepository();
        }

        public override IQueryable<WaterObject> Items
        {
            get
            {
                return _context.WaterObjects.AsQueryable();
            }
        }

        private void InitRepository()
        {
            var waterObjects = GetAllAsync().Result;
            _waterObjects = _cache.GetOrCreate("rivers", () => waterObjects.ToDictionary(x => x.Id));
        }

        public WaterObject GetById(Guid id)
        {
            if (_waterObjects.TryGetValue(id, out var river))
            {
                return river;
            }

            return null;
        }

        public IList<WaterObject> GetAll()
        {
            var result = _waterObjects.Values.ToList();
            return result;
        }
    }
}
