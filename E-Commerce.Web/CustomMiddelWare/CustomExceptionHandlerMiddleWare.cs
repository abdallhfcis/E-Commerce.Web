using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddelWare
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);

                await HandelNotEndPointAsync(httpcontext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");

                await HandelExceptionAsync(httpcontext, ex);

            }
        }

        private static async Task HandelExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            //Set Status Code for Response
            httpcontext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            //Set Content Type  for Response
            httpcontext.Response.ContentType = "application/json";

            //Response Object 
            var response = new ErrorToReurn()
            {
                StatusCode = httpcontext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            //Return Object as Json 
            await httpcontext.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandelNotEndPointAsync(HttpContext httpcontext)
        {
            if (httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReurn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"EndPoint {httpcontext.Request.Path} is Not Found"
                };
                await httpcontext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
