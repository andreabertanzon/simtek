using Microsoft.EntityFrameworkCore;
using Serilog;
using Simtek.Application.Queries;
using Simtek.Application.Repositories;
using Simtek.Application.Repositories.Interfaces;
using Simtek.Blazor.Components;
using Simtek.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<SimtekContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Simtek"));
});

builder.Services.AddSerilog(logger => logger.WriteTo.Console());

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
builder.Services.AddScoped<IInterventionRepository, InterventionRepository>();
builder.Services.AddMediatR(x=> x.RegisterServicesFromAssemblies(typeof(GetCustomersQuery).Assembly));

var app = builder.Build();

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