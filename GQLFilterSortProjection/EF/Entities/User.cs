using GQLSorting.GraphQL.Queries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GQLFilterSortProjection.EF.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        //[SortableField]
        public string Name { get; set; }
        //[SortableField]
        public DateTimeOffset LastUpdated { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
