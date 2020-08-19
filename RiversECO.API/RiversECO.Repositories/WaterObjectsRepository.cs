using System;
using System.Collections.Generic;
using System.Linq;
using RiversECO.Cache.Contracts;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class WaterObjectsRepository : IWaterObjectsRepository
    {
        private readonly IAppMemoryCache _cache;
        private readonly IRiversRepository _riversRepository;
        private readonly ILakesRepository _lakesRepository;

        private IDictionary<Guid, River> _rivers;
        private IDictionary<Guid, Lake> _lakes;

        public WaterObjectsRepository(
            IAppMemoryCache cache,
            IRiversRepository riversRepository,
            ILakesRepository lakesRepository)
        {
            _cache = cache;
            _riversRepository = riversRepository;
            _lakesRepository = lakesRepository;
            
            InitRepository();
        }

        private void InitRepository()
        {
            var rivers = _riversRepository.GetAllAsync().Result;
            _rivers = _cache.GetOrCreate("rivers", () => rivers.ToDictionary(x => x.Id));

            var lakes = _lakesRepository.GetAllAsync().Result;
            _lakes = _cache.GetOrCreate("lakes", () => lakes.ToDictionary(x => x.Id));
        }

        public WaterObject GetById(Guid id)
        {
            if (_rivers.TryGetValue(id, out var river))
            {
                return river;
            }

            if (_lakes.TryGetValue(id, out var lake))
            {
                return lake;
            }

            return null;
        }

        public IList<WaterObject> GetAll()
        {
            var result = new List<WaterObject>();
            result.AddRange(_rivers.Values);
            result.AddRange(_lakes.Values);
            return result;
        }
    }
}
