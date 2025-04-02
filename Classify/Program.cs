using Classify.Components;
using Classify.Security;
using Classify.Services;
using Classify.Common;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AccessTokenService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<APIService>();
builder.Services.AddHttpClient(MicroserviceNames.AuthenticationAPI.GetName(), client => client.BaseAddress = new Uri("https://localhost:7030/api/Authentication/"));
builder.Services.AddHttpClient(MicroserviceNames.GradesAPI.GetName(), client => client.BaseAddress = new Uri("https://localhost:7095/api/Grades/"));
builder.Services.AddHttpClient(MicroserviceNames.EnrollmentAPI.GetName(), client => client.BaseAddress = new Uri("https://localhost:7018/"));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddScheme<CustomOption, JWTAuthenticationProvider>("JWTAuth", options => { });
builder.Services.AddScoped<JWTStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTStateProvider>();
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
var apiUrls = builder.Configuration.GetSection("ApiUrls").Get<Dictionary<string, string>>();
foreach (var apiUrl in apiUrls)
{
    if (string.IsNullOrEmpty(apiUrl.Value))
    {
        throw new InvalidOperationException($"The API URL for {apiUrl.Key} is not configured.");
    }

    builder.Services.AddHttpClient(apiUrl.Key, client =>
    {
        client.BaseAddress = new Uri(apiUrl.Value);
    });
}

builder.Services.AddScoped<GradesService>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
