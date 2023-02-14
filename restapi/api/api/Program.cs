using api.Model;
using api.Repository;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        return new BadRequestObjectResult(new ErrorResponse()
        {
            Errors = context.ModelState.Select(m => $"{m.Key} : {string.Join(Environment.NewLine, m.Value.Errors.Select(v => v.ErrorMessage))}"),
            TraceID = Guid.NewGuid().ToString()
        });
    };
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<IDateTime, SystemDateTime>();

builder.Services.AddMemoryCache();


//builder.Services.Configure<IpRateLimitOptions>(options => builder.Configuration.GetSection("IpRateLimitingSettings").Bind(options));
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.HttpStatusCode = 429;
    options.GeneralRules = new()
    {
        new RateLimitRule()
        {
            Endpoint = "*",
            Period = "10s",
            Limit = 6
        }
    }; 
});

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();