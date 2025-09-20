using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var db = new Db();
builder.Services.AddSingleton<Db>(db);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(x =>
    x.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Addresses",
        Description = "Api for getting and saving addresses in memory",
        Version = "1.0"
    }
));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.WithOrigins("http://localhost:5500",
                                                "http://127.0.0.1:5500")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Swashbuckle and add swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Swashbuckle and add swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
