using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Q1.DTO;
using Q1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DUONGPV14_PRNContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
ODataConventionModelBuilder mdBuilder = new ODataConventionModelBuilder();
mdBuilder.EntitySet<StudentDTO>("Student");
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().Count().OrderBy().AddRouteComponents("odata", mdBuilder.GetEdmModel()));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();