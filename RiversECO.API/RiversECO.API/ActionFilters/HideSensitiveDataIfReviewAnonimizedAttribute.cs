using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RiversECO.Dtos.Responses;

namespace RiversECO.API.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class HideSensitiveDataIfReviewAnonimizedAttribute : ActionFilterAttribute
    {
        private const string PLACEHOLDER = "••••••";

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result as ObjectResult;
            if (result != null)
            {
                if (result.Value is List<ReviewDto>)
                {
                    var reviews = (List<ReviewDto>)result.Value;
                    foreach (var review in reviews)
                    {
                        HideSensitiveData(review);
                    }
                }
                else if (result.Value is ReviewDto)
                {
                    var review = (ReviewDto)result.Value;
                    HideSensitiveData(review);
                }
            }

            base.OnActionExecuted(context);
        }

        private void HideSensitiveData(ReviewDto review)
        {
            if (review.IsAnonymous)
            {
                review.CreatedBy = PLACEHOLDER;
            }
        }
    }
}
