using apiReservas.Seguridad;
using Aplication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var secretkey = builder.Configuration.GetValue<string>("JWT:key");
var key = Encoding.ASCII.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING] 23");
builder.Services.AddCors(o => o.AddPolicy("cors", options =>
{
    options.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();

})
);
ConfigurationManager configuration = builder.Configuration;
var variablesSection = configuration.GetSection("variables");
var puerto = variablesSection["puerto"];

builder.Services.AddControllers(x => x.Filters.Add<AuthorizeAttribute>());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
        };
    });
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

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
    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.WebHost.UseUrls($"{puerto}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "swagger_custom")),
    RequestPath = "/swagger_custom"
});

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/apireserva/swagger/principal/swagger.json", "Inicio");
    c.InjectStylesheet("/apireserva/swagger_custom/prueba.css");
    c.InjectJavascript("/apireserva/swagger_custom/swagger-custom-script.js", "text/javascript");
    //c.HeadContent = @"<link rel=""stylesheet"" type=""text/css"" href=""/reservas2/swagger_custom/swagger-custom-styles.css"">";
    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    c.DefaultModelsExpandDepth(-1);
});

app.UseCors("cors");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
