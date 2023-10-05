using GQLFilterSortProjection.EF.Entities;
using GQLSorting.GraphQL.Types;
using HotChocolate.Data.Sorting;
using HotChocolate.Types;
using System.Linq;
using System.Linq.Expressions;

namespace GQLFilterSortProjection.GraphQL.Sorters
{
    public class OrganizationSortType : SortInputType<Organization> // can say it something like mySortType or whatever blah, blah, blah...
    {
        protected override void Configure(ISortInputTypeDescriptor<Organization> descriptor)
        {
            descriptor.Field(x => x.Id).Ignore();
            //descriptor.Field(x => x.Users).Type<SortEnumType>();
            //descriptor.Field(x => x.Users.Select(y => y.LastUpdated)).Type<SortEnumType>(); // got exception:
            //System.ArgumentException: 'The member expression must specify a property or method that is public and that belongs to the type GQLSorting.EF.Entities.Organization (Parameter 'expression')'
        }
    }
}
