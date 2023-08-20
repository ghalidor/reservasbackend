using Aplication;
using Microsoft.OpenApi.Models;
using Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(o => o.AddPolicy("cors", options =>
{
    options.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();

})
);
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddPersistence();
builder.Services.AddApplication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("principal", new OpenApiInfo
    {
        Title = $"ApiReservas",
        Version = "v2",
        Description = "API Reservas",
        Contact = new OpenApiContact
        {
            Name = "Empresa",
            Email = "reservas@reservas.com",

        }
    });
});

builder.WebHost.UseUrls("http://localhost:8787");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/principal/swagger.json", "Inicio");
    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false); });
app.UseCors("cors");
app.UseAuthorization();

app.MapControllers();

app.Run();
