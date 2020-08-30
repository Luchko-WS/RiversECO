using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;

namespace RiversECO.Repositories
{
    public class ReviewsRepository : DataRepository<Review>, IReviewsRepository
    {

        public ReviewsRepository(DataContext.DataContext context)
            : base(context) { }

        public override IQueryable<Review> Items
        {
            get
            {
                return _context.Reviews.AsQueryable();
            }
        }

        public override void Create(Review model)
        {
            model.Name = $"Review-{model.CreatedBy.Replace(" ", string.Empty)}-{DateTime.UtcNow.Ticks}";
            base.Create(model);
        }

        public async Task<IList<Review>> GetAllForWaterObjectAsync(Guid waterObjectId)
        {
            var items = await Items
                .Where(x => x.WaterObjectId == waterObjectId)
                .ToListAsync();

            return items;
        }
    }
}
