using GQLSorting.GraphQL.Types;
using HotChocolate.Data;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Linq;
using GQLSorting.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;
using GQLSorting.GraphQL.Sorters;
using HotChocolate.Data.Sorting;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using System;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using GQLFilterSortProjection.EF.Entities;
using GQLFilterSortProjection.EF;

namespace GQLFilterSortProjection.GraphQL.Queries
{
    public class Queries
    {
        //private readonly ApiDbContext _context;
        private readonly IConfiguration _configuration;

        public Queries
        (
            //ApiDbContext context, 
            IConfiguration configuration
        )
        {
            _configuration = configuration;
            //_context = context;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //public IQueryable<Organization> Organizations()
        public async Task<List<Organization>> Organizations()
        {
            DbContextOptions<ApiDbContext> options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;

            //using (var dbcontext = new ApiDbContext(options))
            //{
            //    return dbcontext.Organizations.Include(x => x.Users);
            //}

            using var dbcontext = new ApiDbContext(options);

            return await dbcontext.Organizations.Include(x => x.Users).ToListAsync();
        }

        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<Organization> GetSortedOrganizations(SortingInput sortingInput = null)
        {
            DbContextOptions<ApiDbContext> options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;

            using var dbcontext = new ApiDbContext(options);

            IEnumerable<Organization> organizations = dbcontext.Organizations.Include(x => x.Users).ToList();

            if (sortingInput != null)
            {

                var propertyName = sortingInput.Field.ToString();
                var propertyInfo = typeof(User).GetProperty(propertyName);

                if (propertyInfo != null)
                {
                    //organizations = sortingInput.Order == SortingOrder.DESC
                    //    ? organizations = organizations.OrderByDescending(o => o.Users.Max(u => propertyInfo.GetValue(u)))
                    //    : organizations = organizations.OrderBy(o => o.Users.Max(u => propertyInfo.GetValue(u)));

                    organizations = sortingInput.Order == SortingOrder.DESC
                        ? organizations = organizations.OrderByDescending(o => o.Users.Max(u => propertyInfo.GetValue(u)))
                        : organizations = organizations.OrderBy(o => o.Users.Min(u => propertyInfo.GetValue(u)));
                }

                return organizations.AsQueryable();
            }

            return organizations.AsQueryable();
        }

        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<Organization> GetOrganizations1(IResolverContext context)
        {
            var filterArgs = context.ArgumentValue<IDictionary<string, object>>("where");
            var orderArgs = context.ArgumentValue<IDictionary<string, object>>("order");

            DbContextOptions<ApiDbContext> options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;

            using var dbcontext = new ApiDbContext(options);

            return dbcontext.Organizations.Include(x => x.Users).AsQueryable();
        }


        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //public IQueryable<User> getUsers() // on it getting context disposed error.
        public List<User> getUsers()
        {
            DbContextOptions<ApiDbContext> options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;
            //using (var dbcontext = new ApiDbContext(options))
            //{
            //    return dbcontext.Users;
            //}

            using var dbcontext = new ApiDbContext(options);

            return dbcontext.Users.ToList();
        }
    }

    public enum SortingOrder
    {
        ASC,
        DESC
    }

    public enum SortingField
    {
        Name,
        LastUpdated
    }

    public class SortingInput
    {
        public SortingField Field { get; set; }
        public SortingOrder Order { get; set; }
    }
}
