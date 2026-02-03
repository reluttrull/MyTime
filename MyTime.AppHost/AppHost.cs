var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var db = postgres.AddDatabase("mytimedb");

var apiService = builder.AddProject<Projects.MyTime_ApiService>("apiservice")
    .WaitFor(db)
    .WithReference(db)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.MyTime_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
