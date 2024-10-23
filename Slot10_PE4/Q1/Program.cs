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
builder.Services.AddDbContext<SPRING_24_B1Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")));
ODataConventionModelBuilder mdBuilder = new ODataConventionModelBuilder();
builder.Services.AddCors();
mdBuilder.EntitySet<SkillDTO>("Skill");
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().Count().OrderBy().AddRouteComponents("odata", mdBuilder.GetEdmModel()));

var app = builder.Build();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
