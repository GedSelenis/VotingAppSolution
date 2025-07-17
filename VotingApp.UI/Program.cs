using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VotingApp.Core.Domain.RepositoryContracts;
using VotingApp.Core.ServiceContracts;
using VotingApp.Core.Services;
using VotingApp.Infrastructure.DbContext;
using VotingApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPollService, PollService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPollRepository, PollsRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPollOptionsRepository, PollOptionsRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.MapControllers();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
