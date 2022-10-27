using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Application.ViewModels.Contact;
using Application.ViewModels.Login;
using Application.ViewModels.PhoneContact;
using Application.ViewModels.User;
using Domain.Interfaces;
using Infrastructure.Auth;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // validators
            builder.Services.AddScoped<IRequestValidator<UserRequest>, UserRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<LoginRequest>, LoginRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<ContactRequest>, ContactRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<AdminContactRequest>, AdminContactRequestValidator>();
            builder.Services.AddScoped<IRequestValidator<PhoneContactRequest>, PhoneContactRequestValidator>();

            // services
            builder.Services.AddScoped<ILoginService, LoginService>();

            //repos
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();

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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}