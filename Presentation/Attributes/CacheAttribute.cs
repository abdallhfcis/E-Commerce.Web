using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation.Attributes
{
    class CacheAttribute(int DurationInSec=90): ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            //Create Cach Key
            var CacheKey= CreateKey(context.HttpContext.Request);
            //Search For Value with cach Key
            ICacheService cacheService=context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue =await cacheService.GetAsync(CacheKey);
            //Return value if not null
            if (CacheValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            //Return value if Null
            //Invoke .Next
            var ExcutedContext =await next.Invoke();

            //Set Value with Cache Key

            if (ExcutedContext.Result is OkObjectResult result) 
            {
                await cacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
            }

        }

        //{{Baseurl}}/api/Prducts?BrandId=10
        private string CreateKey(HttpRequest request)
        {
            StringBuilder Key= new StringBuilder();
            Key.Append(request.Path+'?');
            foreach (var item in request.Query.OrderBy(Q=> Q.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();
        }
    }
}
