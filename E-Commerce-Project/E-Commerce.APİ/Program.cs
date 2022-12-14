using E_Commerce.Business.AutoMapper;
using E_Commerce.Business.Extensions;
using E_Commerce.Shared.Utilities.Security.Encryption;
using E_Commerce.Shared.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Commerce.API", Version = "1.0.0" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                     Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {{
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            },
            Scheme="oauth2",
            Name="Bearer",
            In=ParameterLocation.Header,
        },
        new List<string>() }
    });
});
IdentityModelEventSource.ShowPII = true;

//builder.Services.AddDistributedRedisCache(option =>
//{
//   option.Configuration = "127.0.0.1:6379";
//});

builder.Services.AddOptions();
//builder.Services.AddMemoryCache();
//builder.Services.AddHttpContextAccessor();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddLogging();


builder.Services.AddAuthorization(options =>
options.AddPolicy("Role",
    policy => policy.RequireClaim(claimType: ClaimTypes.Role, "Admin","Seller","Customer")));

builder.Services.AddAutoMapper(typeof(CustomerProfile), typeof(UserTokenProfile), typeof(BrandProfile), typeof(BrandPictureProfile), typeof(CategoryAndProductProfile), typeof(CommentProfile), typeof(CustomerPictureProfile),typeof(ProductProfile), typeof(ProductPictureProfile),
    typeof(ReportPictureProfile), typeof(ReportProfile), typeof(SellerPictureProfile), typeof(SellerProfile), typeof(ShoppingCartProfile));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.LoadMyServices(builder);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.UseHttpsRedirection();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        builder.Environment.ContentRootPath + "/wwwroot" + "/Uploads"),
//    //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
//    RequestPath = new PathString("/Uploads"),
//});

app.UseAuthorization();
app.MapControllers();

app.Run();
