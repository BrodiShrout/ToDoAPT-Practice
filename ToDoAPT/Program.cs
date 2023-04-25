using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("OriginPolicy", "http://todo.brodishrout.com", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//STEP 04.Add the ResourcesContext Service
//This will initialize the datebase connection to be used in the controller
builder.Services.AddDbContext<ToDoAPT.Models.ToDoContext>(
     Option =>
     {
     Option.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));
                        //the string above should match the connection sttring name in appsettings.json
      }
);



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

app.UseCors();

app.Run();

