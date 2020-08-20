using System.Linq;
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
    }
}
