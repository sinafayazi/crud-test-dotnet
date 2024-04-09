using System.ComponentModel;
using System.Text.Json.Serialization;
using CustomerService;
using CustomerService.Api.Extensions.DependencyInjection;
using CustomerService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguredDatabase(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddConfiguredMediatR();

builder.Services.AddConfiguredHealthChecks();
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.RunMigrations();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
