using GQLSorting.EF.Entities;
using System;

namespace GQLFilterSortProjection.DTOs
{
    public class OrgDTO
    {
        public Guid Id { get; set; }
        public class Query
        {
            public string Name { get; set; }
            public Guid UserId { get; set; }
        }
    }
}
