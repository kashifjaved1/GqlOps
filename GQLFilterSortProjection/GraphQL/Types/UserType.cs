using HotChocolate.Types;
using System;

namespace GQLFilterSortProjection.GraphQL.Types
{
    [ObjectType]
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }
}
