using AspNetCore.Proxy;
using DachaMentat.Config;
using DachaMentat.Executors;
using DachaMentat.Services;

public class DachaMentatProgram
{
    static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var argParser = new ArgumentParser(args);
        var proxySetting = argParser.Parse();
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        if (proxySetting.WorkAsProxy) { 
            builder.Services.AddProxies();
        }

        InitDependencyInjection(builder);

        var app = builder.Build();


        var settings = builder.Configuration.GetSection("OperationSettings");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        if (proxySetting.WorkAsProxy)
        {
            app.RunProxy(proxy => proxy.UseHttp(proxySetting.ProxySettings.BaseUrl));
        }

        app.Run();
    }

    static void InitDependencyInjection(WebApplicationBuilder builder)
    {
        var connectionString = "Data Source=mentat.db";
        builder.Services.AddSingleton<IDataSourceService>(x => new DataSourceService(connectionString));
        builder.Services.AddSingleton<SensorService>(x => new SensorService(x.GetRequiredService<IDataSourceService>()));
        //builder.Services.AddTransient<IndicationService>(x => new IndicationService(x.GetRequiredService<MentatSensorsDbContext>()));
        builder.Services.AddSingleton<IndicationService>(x => new IndicationService(x.GetRequiredService<IDataSourceService>()));
        builder.Services.AddSingleton<ISensorControllerExecutor>(x => new SensorControllerExecutor(x.GetRequiredService<SensorService>()));
        builder.Services.AddSingleton<IIndicationControllerExecutor>(x => new IndicationControllerExeсutor(x.GetRequiredService<IndicationService>(), x.GetRequiredService<SensorService>()));
    }
}