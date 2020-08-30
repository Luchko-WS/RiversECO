using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiversECO.Models;

namespace RiversECO.Contracts.Repositories
{
    public interface IReviewsRepository : IDataRepository<Review>
    {
        Task<IList<Review>> GetAllForWaterObjectAsync(Guid waterObjectId);
    }
}
