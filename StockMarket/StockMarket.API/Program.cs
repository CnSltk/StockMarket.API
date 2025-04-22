using Microsoft.OpenApi.Models;
using StockMarket.Models;
using StockMarket.Services;
using StockMarket.StockMarket.Models.StockMarket.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new OpenApiInfo{Title = "StockMarket API", Version = "v1"});
});

builder.Services.AddSingleton<ICompanyService, CompanyService>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//get all companies
app.MapGet("/api/companies", async (ICompanyService companyService) =>
{
   var companies = await companyService.GetAllAsync();
   return Results.Ok(companies);
});

//get by id
app.MapGet("/api/companies/{id}", async (int id, ICompanyService companyService) =>
{
    var company = await companyService.GetByIdAsync(id);
    return company is not null ? Results.Ok(company) : Results.NotFound();
});

//add a company
app.MapPost("/api/companies", async (Company Company, ICompanyService companyService) =>
{
await companyService.AddAsync(Company);
return Results.Created($"/api/companies/{Company.Id}", Company);
});

//delete a company by id
app.MapDelete("/api/company{id}", async (int id, ICompanyService companyService) =>
{
    await companyService.DeleteAsync(id);
    return Results.Ok();
});
app.Run();