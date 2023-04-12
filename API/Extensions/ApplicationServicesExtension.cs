
using BusinessLogic.Repositories;
using CuentasPorCobrar.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static System.Console;


namespace API.Extensions;

public static class ApplicationServicesExtension
{
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        //services.AddCuentasContext();
       
         services.AddDbContext<CuentasporcobrardbContext>(options => {



                options.UseSqlServer(config.GetConnectionString("PRODUCTION"));


                //  options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
           
        

        services.AddControllers(options =>
        {}).
            AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            }).AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        // Add services to the container.
        services.AddCors(opt => {
            opt.AddPolicy("CorsPolicy", policy => {
                policy
                 .WithOrigins("https://cuentas-por-cobrar-frontend.azurewebsites.net").
                 AllowAnyHeader()
                .AllowAnyMethod();
               // .AllowCredentials();
               

       
            });
        });

        
  
        services.AddEndpointsApiExplorer();


        services.AddSwaggerGen(doc =>
        {
            doc.SwaggerDoc("v1", new()
            {
                Title="Cuentas Por Cobrar Service API",
                Version="v1"
            });


        });
        services.AddScoped<IRepository<Document>, DocumentRepository>();
        services.AddScoped<IRepository<Customer>, CustomerRepository>();
        services.AddScoped<IRepository<AccountingEntry> , AccountingEntryRepository>();
        services.AddScoped<IFilterRepository<Transaction>, TransactionRepository>();
        services.AddScoped<IRepository<Transaction>, TransactionRepository>();
       

        services.AddScoped<IValidator<Document>, DocumentValidator>();
        services.AddScoped<IValidator<AccountingEntry>, AccountingEntriesValidator>();

        //builder.Services.AddScoped<ValidationFilterAttribute>();

        //builder.Services.Configure<ApiBehaviorOptions>(options =>
        //options.SuppressModelStateInvalidFilter=true); 
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
        services.AddScoped<IValidator<Transaction>, TransactionValidator>();

        return services;

    }
    
}

