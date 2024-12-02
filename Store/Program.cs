using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Store.Repository.DbConfig;
using Store.Repository.Repositories;
using Store.Repository.Repositories.Impl;
using Store.Service.Services;
using Store.Service.Services.Impl;

namespace Store.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // MySql
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(9, 1, 0)));
        });

        // Mongo Db
        builder.Services.Configure<MongoDbConfig>(
        builder.Configuration.GetSection("MongoDB"));
        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IMongoClientRepository, MongoClientRepository>();
        builder.Services.AddMemoryCache();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}