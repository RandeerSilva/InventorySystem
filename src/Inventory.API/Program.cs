using Inventory.Application;
using Inventory.Infrastructure;
using Inventory.Application.Features.Categories.Commands.CreateCategory;
using Inventory.Application.Features.Products.Commands;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapPost("/categories",
    async (CreateCategoryCommand cmd, IMediator mediator) =>
        Results.Ok(await mediator.Send(cmd)));

app.MapPost("/products",
    async (CreateProductCommand cmd, IMediator mediator) =>
        Results.Ok(await mediator.Send(cmd)));
app.UseAuthorization();

app.MapControllers();

app.Run();
