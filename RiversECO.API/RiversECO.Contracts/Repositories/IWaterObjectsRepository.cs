using System;
using System.Collections.Generic;
using RiversECO.Models;

namespace RiversECO.Contracts.Repositories
{
    public interface IWaterObjectsRepository
    {
        WaterObject GetById(Guid id);
        IList<WaterObject> GetAll();
    }
}
