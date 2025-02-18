namespace HotChocolate.Types;

internal class ErrorInterfaceType : InterfaceType
{
    protected override void Configure(IInterfaceTypeDescriptor descriptor)
    {
        descriptor.Name("Error");
        descriptor.Field("message").Type<NonNullType<StringType>>();
    }
}
