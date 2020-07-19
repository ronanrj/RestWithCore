using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestCore.Business;
using RestCore.Business.Implementations;
using RestCore.Model.Context;
using RestCore.Repository;
using RestCore.Repository.Implementations;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using RestCore.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using RestCore.Hypermedia;
using RestCore.Security.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace RestCore
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IHostingEnvironment _environment { get; }
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration , IHostingEnvironment environment , ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;

        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["SqlConnection:SqlConnectionString"];

            services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure()));


            if (_environment.IsDevelopment())
            {
                try
                {

                    var evolveConnection = new SqlConnection(connectionString);


                    //var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    //{                        
                    //    Locations = new[] { "db/migrations" },
                    //    IsEraseDisabled = true,
                    //    Command = Evolve.Configuration.CommandOptions.Migrate,
                    //    Placeholders = new Dictionary<string, string>
                    //    {
                    //        ["${db}"] = "rest_with_aspnet_udemy",
                    //        ["${schema2}"] = "dbo"
                    //    }

                    //};
                    //evolve.Migrate();
                }
                catch (Exception ex)
                {

                    _logger.LogCritical("Database migration failed.", ex);
                    throw;
                }

            }




            #region Autenticacao

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });



            #endregion



            //content Negociation -- inserindo 
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<ILoginBusiness, LoginBusinessImpl>();

            services.AddScoped<ILoginRepository, LoginRepositoryImpl>();


            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));



            //versionando

            services.AddApiVersioning();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}");
            });
        }
    }
}
