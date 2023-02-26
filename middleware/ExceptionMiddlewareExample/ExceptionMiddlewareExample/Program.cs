using ExceptionMiddlewareExample.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseMiddleware<OneMiddleware>();
app.UseMiddleware<TwoMiddleware>();
app.UseMiddleware<ExceptionsMiddleware>();

app.Use(async (context, next) =>
{
    Console.WriteLine("use method!");
    await next();
});

app.Run(async context =>
{
    await context.Response.WriteAsJsonAsync("end!");
});

app.Run();
