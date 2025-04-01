var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Classify>("classify");

builder.AddProject<Projects.Authentication>("authentication");

builder.Build().Run();
