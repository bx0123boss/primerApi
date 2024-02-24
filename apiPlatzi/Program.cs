using apiPlatzi.Services;
using apiPlatzi;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace apiPlatzi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSqlServer<TareasContext>("Data Source = WS-DSOF06; Initial Catalog = TareasDb; user id = sa; password = admin123; TrustServerCertificate = True");
            //builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
            builder.Services.AddScoped<IHelloWorldService>(p => new HelloWorldService());
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<ITareaService, TareaService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //Poner pagina de bienvenida
            //app.UseWelcomePage();
            //Middleware custom
            //app.UseTimeMiddleware();
            app.MapControllers();

            app.Run();
        }
    }
}
