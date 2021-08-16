using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using dndDatabaseAPI.Repositories;
using dndDatabaseAPI.Spells.Repositories;
using Npgsql;
using dndDatabaseAPI.Settings;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using dndDatabaseAPI.Authorization;
using dndDatabaseAPI.Services.Users;
using dndDatabaseAPI.Helpers;
using dndDatabaseAPI.Models.Users;
using dndDatabaseAPI.Repositories.Users;
using System.Collections.Generic;
using dndDatabaseAPI.Repositories.Character;

namespace dndDatabaseAPI
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            // Slugify value
            var stringValue = value.ToString();
            return value == null ? null : Char.ToLowerInvariant(stringValue[0]) + stringValue[1..];
        }
    }

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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(mongoDbSettings.ConnectionString);
            });

            AddRepositorires(services);

            // configure strongly typed settings object
            services.Configure<JWTSettings>(Configuration.GetSection(nameof(JWTSettings)));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            AddSwaggerDocumentation(services);

            services
                .AddHealthChecks()
                .AddMongoDb(
                    mongoDbSettings.ConnectionString,
                    name: "mongodb",
                    timeout: TimeSpan.FromSeconds(10),
                    tags: new[] { "database", "mongodb" }
                );

            services
                .AddHealthChecksUI(settings =>
                {
                    //settings.SetEvaluationTimeInSeconds(10);
                    //settings.AddHealthCheckEndpoint("all", "/health/all");
                    //settings.AddHealthCheckEndpoint("database", "/health/ready");
                })
                .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUsersRepository usersRepository)
        {
            CreateTestUser(usersRepository);

            UseSwaggerDocumentation(app, env);

            if (env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            UseJWT(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                //{
                //    Predicate = (check) => check.Tags.Contains("database"),
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});

                //endpoints.MapHealthChecks("/health/all", new HealthCheckOptions
                //{
                //    Predicate = (_) => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});

                //endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                //{
                //    Predicate = (_) => false,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});

                endpoints.MapHealthChecksUI();
            });
        }

        private static void AddRepositorires(IServiceCollection services)
        {
            services.AddSingleton<IUsersRepository, InMemUsersRepository>();
            //services.AddSingleton<ISpellsRepository, MongoDbSpellsRepository>();
            services.AddSingleton<ISpellsRepository, InMemSpellsRepository>();
            services.AddSingleton<IClassesRepository, InMemClassesRepository>();
        }

        private static void AddSwaggerDocumentation(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dndDatabaseAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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
        }

        private static void UseJWT(IApplicationBuilder app)
        {
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
        }

        private static void UseSwaggerDocumentation(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dndDatabaseAPI v1"));
            }
        }

        private static async void CreateTestUser(IUsersRepository usersRepository)
        {
            // add hardcoded test user to db on startup
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User",
                Username = "test",
                PasswordHash = BCryptNet.HashPassword("test"),
                RefreshTokens = new List<RefreshToken>(),
            };
            await usersRepository.CreateAsync(testUser);
        }
    }
}
