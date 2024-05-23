using AspNetCore.Proxy;
using DachaMentat.Config;
using DachaMentat.Executors;
using DachaMentat.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

        builder.Services.AddAuthentication();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "BasePolicy",
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:4200",
                                                      "https://localhost:4200");
                              });
        });

        AddJwtTokenSupport(builder.Services);

        InitDependencyInjection(builder);

        var app = builder.Build();


        var settings = builder.Configuration.GetSection("OperationSettings");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        if (proxySetting.WorkAsProxy)
        {
            app.RunProxy(proxy => proxy.UseHttp(proxySetting.ProxySettings.BaseUrl));
        }

        app.Run();
    }

    private static void AddJwtTokenSupport(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,                    
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }

    private static void InitDependencyInjection(WebApplicationBuilder builder)
    {
        var connectionString = "Data Source=mentat.db";
        builder.Services.AddScoped<IUserAuthService, UserAuthService>();
        builder.Services.AddSingleton<IDataSourceService>(x => new DataSourceService(connectionString));
        builder.Services.AddSingleton<SensorService>(x => new SensorService(x.GetRequiredService<IDataSourceService>()));
        //builder.Services.AddTransient<IndicationService>(x => new IndicationService(x.GetRequiredService<MentatSensorsDbContext>()));
        builder.Services.AddSingleton<IndicationService>(x => new IndicationService(x.GetRequiredService<IDataSourceService>()));
        builder.Services.AddSingleton<ISensorControllerExecutor>(x => new SensorControllerExecutor(x.GetRequiredService<SensorService>()));
        builder.Services.AddSingleton<IIndicationControllerExecutor>(x => new IndicationControllerExeсutor(x.GetRequiredService<IndicationService>(), x.GetRequiredService<SensorService>()));
    }
}