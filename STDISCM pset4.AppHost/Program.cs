var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Classify>("classify");

builder.AddProject<Projects.Authentication>("authentication");

builder.AddProject<Projects.Enrollment>("enrollment");

builder.AddProject<Projects.Grades>("grades");

builder.Build().Run();
