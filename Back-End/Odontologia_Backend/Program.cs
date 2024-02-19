using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Odontologia_Backend.Data;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// conexion a mysql
builder.Services.AddDbContext<Context_MySql>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")));

// implementando jsonWebToken
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// fin configuracion jsonWebToken

/* esta instrucción configura los controladores para que serialicen los objetos JSON sin aplicar ninguna transformación especial en los nombres de las propiedades, utilizando los nombres de propiedad exactos definidos en las clases. Esto puede ser útil si deseas que los nombres de las propiedades en el JSON coincidan exactamente con los nombres de las propiedades en las clases C#. */
builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rest API Odontologia",
        Version = "v1",
        Description = "Aplication_Back_End_Odontologia",
        Contact = new OpenApiContact()
        {
            Name = "Jesús Morales",
            Email = "alfonz2310@gmail.com"
        }
    });

    // configuracion para agregar el token al swagger
    // boton autorize (Swagger)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
   });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true); //EXCEPTION ---*****----
    c.TagActionsBy(api =>
    {
        string tag;
        if (api.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            var attribute = descriptor.EndpointMetadata.OfType<ApiExplorerSettingsAttribute>().FirstOrDefault();
            tag = attribute?.GroupName ?? descriptor.ControllerName;
        }
        else
        {
            tag = api.GroupName;
        }

        var tags = new List<string>();
        if (!string.IsNullOrEmpty(tag))
        {
            tags.Add(tag);
        }
        return tags;
    });
    c.DocInclusionPredicate((name, api) => true);
});

// Configuracion de CORS
builder.Services.AddCors(op =>
{
    op.AddPolicy(name: "MyPolicy",
        builder =>
        {
            //builder.WithOrigins("http://190.124.28.35:4200/#/home",
            //                    "https://localhost:44398",
            //                    "https://localhost:5001",
            //                    "https://ph.airtek.com.ve:994")
            //.AllowAnyHeader()
            //.WithMethods("POST");

            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// for production
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest_Api_Odontologia v1"));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRouting();

app.UseCors("MyPolicy"); // Habilito el uso de CORS

app.MapControllers();

app.Run();
