using Classify.Components;
using Classify.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
    .AddIdentityCookies();
builder.Services.AddScoped<CookieEvents>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.EventsType = typeof(CookieEvents);
});

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

builder.Services.AddScoped<CourseService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("EnrollmentApi");
    return new CourseService(httpClient);
});

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
