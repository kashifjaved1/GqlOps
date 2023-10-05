using GQLFilterSortProjection.EF;
using GQLFilterSortProjection.EF.Entities;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using System;

namespace GQLFilterSortProjection.GraphQL.Mutations
{
    [ExtendObjectType(Name = "Mutation")]
    [Obsolete]
    public class Mutation
    {
        //private readonly ApiDbContext _context;

        public Mutation(ApiDbContext context)
        {
            //_context = context;
        }

        [UseDbContext(typeof(ApiDbContext))]
        [UseProjection]//          |
        [UseSorting]//            \|/
        public User AddUser([ScopedService] ApiDbContext _context, User user) // todo: use some dto/input here later on to take just username.
        {
            user.Id = Guid.NewGuid();
            user.LastUpdated = DateTime.UtcNow;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        [UseDbContext(typeof(ApiDbContext))]
        public Organization AddOrganization([ScopedService] ApiDbContext _context, Organization organization)
        {
            // as dto isn't configured to take & handle some user-based values, so that's why I'm manually assigning some values.
            organization.Id = Guid.NewGuid();
            _context.Organizations.Add(organization);
            _context.SaveChanges();
            return organization;
        }
    }
}
