using GQLFilterSortProjection.DTOs;
using GQLFilterSortProjection.EF;
using GQLFilterSortProjection.GraphQL.Mutations;
using GQLFilterSortProjection.GraphQL.Types;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Queries = GQLFilterSortProjection.GraphQL.Queries.Queries;

namespace GQLFilterSortProjection
{
    public static class ServiceExtensions
    {
        public static void ProjectSettings(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddCors(corsPolicy =>
            {
                corsPolicy.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GQLSorting", Version = "v1" });
            //});

            services.AddGraphQLServer()
                .AddQueryType<Queries>()
                .AddMutationType<Mutation>()
                .AddType<OrganizationType>()
                .AddType<UserType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .ConfigureSchema(schema =>
                {
                    schema.AddObjectType<OrgDTO>();
                });

            //services.AddDbContext<ApiDbContext>(o => o.UseSqlServer(connectionString));

            services.AddPooledDbContextFactory<ApiDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}