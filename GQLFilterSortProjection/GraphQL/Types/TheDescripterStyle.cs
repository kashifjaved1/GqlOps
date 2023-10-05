using GQLFilterSortProjection.EF.Entities;
using HotChocolate.Types;

namespace GQLFilterSortProjection.GraphQL.Types
{
    public class TheDescripterStyle
    {
        // DS
    }

    // DS = TheDescripterStyle
    public class UserTypeDS : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            //base.Configure(descriptor);
            descriptor.Field(o => o.Id).Type<IntType>();
            descriptor.Field(o => o.Name).Type<NonNullType<StringType>>();
            descriptor.Field(o => o.LastUpdated).Type<NonNullType<DateTimeType>>();
        }
    }

    // DS = TheDescripterStyle
    public class OrganizationTypeDS : ObjectType<Organization>
    {
        protected override void Configure(IObjectTypeDescriptor<Organization> descriptor)
        {
            //base.Configure(descriptor);
            descriptor.Field(o => o.Id).Type<IntType>();
            descriptor.Field(o => o.Name).Type<NonNullType<StringType>>();
            descriptor.Field(o => o.Users).Type<NonNullType<UserTypeDS>>().UseFiltering();
        }
    }
}
