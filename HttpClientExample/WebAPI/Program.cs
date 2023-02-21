using WeatherForecast.Controllers;
using WebAPI.Http;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// OneController.cs
builder.Services.AddHttpClient();

// TwoController.cs
builder.Services.AddHttpClient("BookStore", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7039/");
});

builder.Services.AddHttpClient("WeatherForecast", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7104/");
});

// ThreeController.cs
builder.Services.AddHttpClient<ThreeController>();

// ForeController.cs - Refit
builder.Services.AddRefitClient<IBookStoreClient>()
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7039/");
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
