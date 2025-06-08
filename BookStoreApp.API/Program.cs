using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

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

app.UseCors("AllowAll"); // CORS must be before both authentication and authorization
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();

app.Run();
