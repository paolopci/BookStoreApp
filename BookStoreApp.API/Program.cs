using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("BookStoreAppDbConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(connString)
);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",// da ogni dove nome che do alla mia policy
        b => b.AllowAnyMethod() // ogni metodo Get,post,put,delete
            .AllowAnyHeader()   // ogni header
            .AllowAnyOrigin()); // ogni origine
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");// cors va  before both authentication and authorization. 
app.UseAuthorization();

app.MapControllers();

app.Run();
