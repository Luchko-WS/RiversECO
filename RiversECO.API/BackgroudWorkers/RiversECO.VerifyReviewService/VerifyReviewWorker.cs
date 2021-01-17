using System;
using System.Linq;
using System.Threading.Tasks;
using RiversECO.Contracts.Repositories;
using RiversECO.Models;
using RiversECO.PlainTextExtractors;

namespace RiversECO.BackgroudWorkers.VerifyReviewService
{
    public class VerifyReviewWorker
    {
        private IReviewsRepository _repository;

        public VerifyReviewWorker(IReviewsRepository repository)
        {
            _repository = repository;
        }

        public async Task DoWork()
        {
            do
            {
                var pendingApproveReviews = await _repository.GetAllPendingApprovalReviews();
                if (pendingApproveReviews.Any())
                {
                    foreach (var review in pendingApproveReviews)
                    {
                        var uri = new Uri(review.References);
                        review.Status = await CheckUri(uri) ?
                            ReviewStatus.Approved :
                            ReviewStatus.NotApproved;
                    }
                    await _repository.SaveAllChangesAsync();
                }
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
            while (true);
        }

        // TODO: STUB
        private async Task<bool> CheckUri(Uri uri)
        {
            var parser = new UrlExtractor(uri);
            var text = await parser.ExtractPlainTextAsync();
            return !string.IsNullOrEmpty(text);
        }
    }
}
