var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FastEndpoint_Api>("fasterEndpointAPI");

builder.Build().Run();