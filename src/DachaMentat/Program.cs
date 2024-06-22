using AspNetCore.Proxy;
using DachaMentat.Config;
using DachaMentat.Db;
using DachaMentat.Exceptions;
using DachaMentat.Executors;
using DachaMentat.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class DachaMentatProgram
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var operationalSettings = InitOperationalSettings(builder);

        //var argParser = new ArgumentParser(args);
        //var proxySetting = argParser.Parse();
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Configuration.AddEnvironmentVariables();
        builder.Configuration.AddCommandLine(args);
        builder.Services.AddSwaggerGen();

        if (operationalSettings.WorkAsProxy)
        {
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

        if (operationalSettings.WorkAsProxy)
        {
            app.RunProxy(proxy => proxy.UseHttp(operationalSettings.ProxySettings.BaseUrl));
        }

        app.Run();
    }

    private static DbConnectionSettings InitDbSettings(WebApplicationBuilder builder)
    {
#if DEBUG
        var dbCon = builder.Configuration.GetSection("DBConnectionSettings");

        var res = new DbConnectionSettings() {
            ConnectionString = dbCon.GetValue<string>("ConnectionString"),
            DatabaseType = dbCon.GetValue<DbType>("DbType")
        };
#else
        var res = new DbConnectionSettings() {
            ConnectionString = builder.Configuration.GetValue<DbType>("MentatConnectionString"),
            DatabaseType = builder.Configuration.GetValue<DbType>("MentatDbType")
        };
#endif   

        return res;
    }

    private static OperationalSettings InitOperationalSettings(WebApplicationBuilder builder)
    {
        var oper = builder.Configuration.GetSection("OperationalSettings");

        var settings = new OperationalSettings()
        {
            WorkAsProxy = oper.GetValue<bool>("WorkAsProxy"),
            ProxySettings = new ProxySettings()
            {
                BaseUrl = oper.GetSection("ProxySettings").GetValue<string>("BaseUrl"),
            }
        };

        return settings;           
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
        var dbSettings = InitDbSettings(builder);

       // var connectionString = "Data Source=mentat.db";
      //  var dbType = DbType.Sqlite;
        var dbInit = CreateDbInitCode(dbSettings.DatabaseType, dbSettings.ConnectionString);

        builder.Services.AddScoped<IUserAuthService, UserAuthService>();
        builder.Services.AddTransient<MentatSensorsDbContext>(x => new MentatSensorsDbContext(dbInit));
        builder.Services.AddTransient<MentatUsersDbContext>(x => new MentatUsersDbContext(dbInit));
        builder.Services.AddTransient<IDataSourceService>(x => new DataSourceService(x.GetRequiredService<MentatSensorsDbContext>(), x.GetRequiredService<MentatUsersDbContext>()));
        builder.Services.AddTransient<SensorService>(x => new SensorService(x.GetRequiredService<IDataSourceService>()));
        //builder.Services.AddTransient<IndicationService>(x => new IndicationService(x.GetRequiredService<MentatSensorsDbContext>()));
        builder.Services.AddTransient<IndicationService>(x => new IndicationService(x.GetRequiredService<IDataSourceService>()));
        builder.Services.AddTransient<ISensorControllerExecutor>(x => new SensorControllerExecutor(x.GetRequiredService<SensorService>()));
        builder.Services.AddTransient<IIndicationControllerExecutor>(x => new IndicationControllerExeсutor(x.GetRequiredService<IndicationService>(), x.GetRequiredService<SensorService>()));
    }

    private static DbInitDelegate CreateDbInitCode(DbType dbType, string connectionString)
    {
        if (dbType == DbType.Sqlite)
        {
            return (DbContextOptionsBuilder optionsBuilder) =>
            {
                optionsBuilder.UseSqlite(connectionString);
            };
        }

        if (dbType == DbType.MySql)
        {
            return (DbContextOptionsBuilder optionsBuilder) =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
                optionsBuilder.UseMySql(connectionString, serverVersion);
            };
        }

        return (DbContextOptionsBuilder optionsBuilder) => {
            throw new MentatDbException("DB Was not Specified");
        };
    }
}