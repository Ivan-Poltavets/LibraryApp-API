using LibraryApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MySQL");
//builder.WebHost.UseKestrel(options =>
//{
//    options.Listen(IPAddress.Any,
//        Convert.ToInt32(Environment.GetEnvironmentVariable("PORT")));

//});
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySQL(connectionString);
});
builder.Services.AddData();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddCors(x =>
{
    x.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
