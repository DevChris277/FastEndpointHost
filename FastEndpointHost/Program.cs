var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db").WithPgAdmin();

var fastEndpointDb = db.AddDatabase("fastEndpointDb");

builder.AddProject<Projects.FastEndpoint_Api>("fastEndpointAPI")
    .WithReference(fastEndpointDb)
    .WaitFor(fastEndpointDb);

builder.Build().Run();