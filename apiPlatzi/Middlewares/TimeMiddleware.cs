public class TimeMiddleware
{
    readonly RequestDelegate next;
    public TimeMiddleware(RequestDelegate nextRequest)
    {
        this.next = nextRequest;
    }

    public async Task Invoke(HttpContext context)
    {
        await next(context);
        if (context.Request.Query.Any(p => p.Key == "time")) 
        { 
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());    
        }
        
    }

}
public static class TimeMiddlewareExtension
{
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TimeMiddleware>();
    }
}