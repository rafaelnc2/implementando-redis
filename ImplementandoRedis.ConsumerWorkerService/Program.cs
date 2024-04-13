using ImplementandoRedis.ConsumerWorkerService.Extensions;

var builder = Host.CreateApplicationBuilder(args);

var config = builder.Configuration;

builder.Services.AddBootstrapperRegistration(config);

builder.Services.AddWorkers();

var host = builder.Build();

host.Run();
