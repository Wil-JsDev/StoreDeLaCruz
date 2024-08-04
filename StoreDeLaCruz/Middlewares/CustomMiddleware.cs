namespace StoreDeLaCruz.Middlewares
{
    public class CustomMiddleware
    {
        private RequestDelegate _requestDelegate;

        public CustomMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Se ejecuta despues de la request
            await _requestDelegate(httpContext);

            if (true)
            {

            }

            //Se ejecuta antes de la request
            //await _requestDelegate(httpContext);

        }

    }
}
