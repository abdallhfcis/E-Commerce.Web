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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");

                //Set Status Code for ResponSe
                httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                //Set Content Type  for Response
                httpcontext.Response.ContentType = "application/json";

                //Response Object 
                var response = new ErrorToReurn()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };
                //Return Object as Json 
                await httpcontext.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
