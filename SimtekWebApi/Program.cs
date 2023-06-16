using System.Data;
using Carter;
using Npgsql;
using SimtekData.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(SimtekApplication.Handlers.Intervention.GetInterventionsQueryHandler)
        .Assembly);
});

builder.Services.AddTransient<IDbConnection>(db => new NpgsqlConnection(builder.Configuration.GetConnectionString("Marten")));
builder.Services.AddScoped<InterventionRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
app.MapCarter();
//app.MapControllers();

app.Run();