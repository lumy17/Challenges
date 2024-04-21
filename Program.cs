using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Challenges.Data;
using Challenges.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using OpenAI_API;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString
    ("DefaultConnection") ?? throw new InvalidOperationException
    ("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(connectionString));

//adaugam rolurile -- putem avea admin sau user
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddSingleton<OpenAIAPI>();


// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Provocari");
    options.Conventions.AuthorizeFolder("/ProvocariUtilizatori");
    options.Conventions.AuthorizeFolder("/Sarcini");
    options.Conventions.AuthorizePage("/dashboard");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Provocari/Index");
    options.Conventions.AuthorizeFolder("/Sarcini", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/SarciniRealizate", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Realizari", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/RealizariUtilizator", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categorii", "AdminPolicy");
});
builder.Services.AddScoped<RankingService>();

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var config = builder.Configuration;

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = config["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = config["Authentication:Google:ClientSecret"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapRazorPages();
});


app.MapRazorPages();

app.Run();
