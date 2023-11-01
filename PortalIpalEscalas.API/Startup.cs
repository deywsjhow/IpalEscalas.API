using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PortalIpalEscalas.API.Config;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace PortalIpalEscalas.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Authentication", new OpenApiSecurityScheme
                {
                    Description = "Header de Autorization JWT usando o esquema Bearer. Ex: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "auth"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "auth"
                            }
                        }, new List<string>()
                    }
                });
            });

            services.AddInfra(Configuration);

            var host = "https://apiescalasipal-client.com.br";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var validation = bearerOptions.TokenValidationParameters;
                var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(Configuration["JwtKey"]));
                validation.IssuerSigningKey = key;
                validation.ValidAudience = host;
                validation.ValidIssuer = host;

                //Valida a assing de um token
                validation.ValidateIssuerSigningKey = true;

                //Verifica se o token ainda é valido
                validation.ValidateLifetime = true;

                //Tempo de tolerancia de expiração de um token(caso haja problemas de sincronismo entre diferentes comps envolvidos
                validation.ClockSkew = TimeSpan.Zero;
            });

            //Ativa o uso do token como forma de autorizar os recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escalas Ipal API");
            });
            
        }
    }
}
