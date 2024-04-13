using CacheSample.Api.Extensions;
using CacheSample.Api.Filters;
using ImplementandoRedis.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionHandlingFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//https://learn.microsoft.com/en-us/dotnet/core/extensions/queue-service

builder.Services.AddScoped<GlobalExceptionHandlingFilter>();

builder.Services.AddBootstrapperRegistration(builder.Configuration);

builder.Services.AddMediatrService();

builder.Services.AddHostedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();