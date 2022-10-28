using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Application.Services.Admin;
using Application.Validators;
using Application.ViewModels.Contact;
using Application.ViewModels.Login;
using Application.ViewModels.PhoneContact;
using Application.ViewModels.User;
using Domain.Core;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IDP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // configura autenticacao via swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                                Digite 'Bearer' [espaco] e o token de login.
                                \r\n\r\nExemplo: 'Bearer 12345abcdef'",

                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddDbContext<ApplicationContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // auto mapper
            builder.Services.AddAutoMapper(typeof(ModelToResponseProfile), typeof(RequestToModelProfile));

            // validators
            builder.Services.AddScoped<IRequestValidator<UserRequest>, UserRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<LoginRequest>, LoginRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<ContactRequest>, ContactRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<AdminContactRequest>, AdminContactRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<PhoneContactRequest>, PhoneContactRequestValidator>();

            // services
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPhonebookService, PhonebookService>();

            //repos
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            
            // uow
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // auth
            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure<JwtGeneratorOptions>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();

            builder.Services.AddAuthentication(x =>
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
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agenda.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}