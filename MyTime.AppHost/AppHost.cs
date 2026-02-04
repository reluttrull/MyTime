var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var db = postgres.AddDatabase("mytimedb");

builder.AddProject<Projects.MyTime_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WaitFor(db)
    .WithReference(db)
    .WithHttpHealthCheck("/health");

builder.Build().Run();