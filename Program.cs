using Challenges.WebApp.Data;
using Challenges.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString
    ("DefaultConnection") ?? throw new InvalidOperationException
    ("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddSingleton<OpenAIAPI>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/AppChallenges");
    options.Conventions.AuthorizeFolder("/UserChallenges");
    options.Conventions.AuthorizeFolder("/TodoTasks");
    options.Conventions.AuthorizePage("/Dashboards/dashboard");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/AppChallenges/Index");
    options.Conventions.AllowAnonymousToPage("/AppChallenges/Details");
    options.Conventions.AuthorizeFolder("/TodoTasks", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/FinishedTasks", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Badges", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/UserBadges", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
});
builder.Services.AddScoped<RankingService>();

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var config = builder.Configuration;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
