using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Task_Management_Data.Entities.Identity;
using Task_Management_Data.Helper;
using Task_Management_Infrastructure.Data;


namespace Task_Management_Infrastructure
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            //JWT Authentication
            var jwtSettings = new JwtSettings();
            // var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            //  configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);

            services.AddSingleton(jwtSettings);
            //   services.AddSingleton(emailSettings);


            var googleDriveSetting = new GoogleDriveSettings();
            configuration.GetSection("GoogleDrive").Bind(googleDriveSetting);
            services.AddSingleton(googleDriveSetting);


            var googleAuthSettings = new GoogleAuthSettings();
            configuration.GetSection("GoogleAuth").Bind(googleAuthSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = jwtSettings.validateIssuer,
                   ValidIssuers = new[] { jwtSettings.issuer },
                   ValidateIssuerSigningKey = jwtSettings.validateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.secret)),
                   ValidAudience = jwtSettings.audience,
                   ValidateAudience = jwtSettings.validateAudience,
                   ValidateLifetime = jwtSettings.validateLifetime,
               };
           });
            /*.AddGoogle(options =>
            {
                options.ClientId = googleAuthSettings.ClientId;
                options.ClientSecret = googleAuthSettings.ClientSecret;
            });*/



            /* services.AddAuthorization(options =>
             {
                 options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
             });*/
            //Swagger Gn
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task_management System", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
             {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = JwtBearerDefaults.AuthenticationScheme
                 }
             },
             Array.Empty<string>()
             }
           });
            });

            /*  services.AddAuthorization(option =>
              {
                  option.AddPolicy("CreateStudent", policy =>
                  {
                      policy.RequireClaim("Create Student", "True");
                  });
                  option.AddPolicy("DeleteStudent", policy =>
                  {
                      policy.RequireClaim("Delete Student", "True");
                  });
                  option.AddPolicy("EditStudent", policy =>
                  {
                      policy.RequireClaim("Edit Student", "True");
                  });
              });*/



            return services;
        }
    }
}