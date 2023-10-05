using HotChocolate.Types;
using System.Collections.Generic;

namespace GQLFilterSortProjection.GraphQL.Types
{
    [ObjectType]
    public class OrganizationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserType> Users { get; set; }
    }
}