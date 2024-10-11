using API.DTO;
using Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Presentation;
using Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FUNewsManagementDbContext>(opt
    => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISystemAccountRepo, SystemAccountRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INewArticlesService, NewArticlesService>();
builder.Services.AddScoped<INewsArticleRepo, NewArticlesRepoRepository>();

builder.Services.AddSwaggerGen(setup => {
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

builder.Services.AddControllers().AddOData(options => {
    options.EnableQueryFeatures()
        .AddRouteComponents("odata", GetEdmModel()); // Register OData routes
});

static IEdmModel GetEdmModel() {
    var odataBuilder = new ODataConventionModelBuilder();

    var newSet = odataBuilder.EntitySet<NewsArticle>("Articles");
    newSet.EntityType.HasKey(a => a.NewsArticleId);

    return odataBuilder.GetEdmModel();
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSection:SecretKey").Value)),
            ValidIssuer = builder.Configuration.GetSection("JWTSection:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("JWTSection:Audience").Value,
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
