using Multi_Tenant_API.Infrastructure.Data;

namespace Multi_Tenant_API.API.Middleware
{
    public class TenantContextMiddleware
    {

        //add a middleware component to the pipeline 
        private readonly RequestDelegate _next;

        public TenantContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //httpcontext (contains all the details about the ongoing request)
        public async Task InvokeAsync(HttpContext context, TenantResolver tenantResolver)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var tenantIdClaim = context.User.FindFirst("TenantId");
                if (tenantIdClaim != null && int.TryParse(tenantIdClaim.Value, out int tenantId))
                {
                    tenantResolver.SetTenantId(tenantId); // Save it in scoped resolver
                }
            }

            await _next(context);   //continue to the next component to the pipeline
        }
    }
}
