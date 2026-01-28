using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<IFilesServices, FileServices>();
builder.Services.AddScoped<IActorsServices, ActorsServices>();
builder.Services.AddScoped<IUserCommentsServices, UserCommentsServices>();
builder.Services.AddScoped<IFavoriteListsServices, FavoriteListsServices>();
builder.Services.AddScoped<IEmailsServices, EmailsServices>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountsServices, AccountsServices>();
builder.Services.AddScoped<IWeatherForcastServices, WeatherForecastServices>();
builder.Services.AddHttpClient<IOMDbApiServices, OMDbApiServices>();
builder.Services.AddSignalR();
builder.Services.AddDbContext<FilminurkTARpe24Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
{
    Options.SignIn.RequireConfirmedAccount = true;
    Options.Password.RequiredLength = 8;

    Options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
    Options.Lockout.MaxFailedAccessAttempts = 5;
    Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //Options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<FilminurkTARpe24Context>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<Filminurk.Hubs.ChatHub>("/chathub");

app.Run();
