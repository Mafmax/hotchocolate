using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HotChocolate.Configuration;
using HotChocolate.Internal;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using HotChocolate.Types.Relay;

namespace HotChocolate.Data.Filters;

public class FilterTypeInterceptor
    : TypeInterceptor
{
    private readonly Dictionary<string, IFilterConvention> _conventions = new();

    public override bool CanHandle(ITypeSystemObjectContext context) => true;

    public override void OnBeforeRegisterDependencies(
        ITypeDiscoveryContext discoveryContext,
        DefinitionBase? definition,
        IDictionary<string, object?> contextData)
    {
        if (definition is FilterInputTypeDefinition def)
        {
            IFilterConvention convention = GetConvention(
                discoveryContext.DescriptorContext,
                def.Scope);

            SchemaTypeReference typeReference = TypeReference.Create(
                discoveryContext.Type,
                def.Scope);

            var descriptor = FilterInputTypeDescriptor.New(
                discoveryContext.DescriptorContext,
                def.EntityType,
                def.Scope);

            ApplyCorrectScope(def, discoveryContext);

            convention.ApplyConfigurations(typeReference, descriptor);

            FilterInputTypeDefinition extensionDefinition = descriptor.CreateDefinition();

            ApplyCorrectScope(extensionDefinition, discoveryContext);

            discoveryContext.RegisterDependencies(extensionDefinition);

            foreach (InputFieldDefinition field in def.Fields)
            {
                if (field is FilterFieldDefinition filterField &&
                    filterField.Member?.GetCustomAttribute(typeof(IDAttribute)) != null)
                {
                    filterField.Type = discoveryContext.TypeInspector.GetTypeRef(
                        typeof(IdOperationFilterInputType),
                        TypeContext.Input,
                        discoveryContext.Scope);
                }
            }
        }
    }

    public override void OnBeforeCompleteName(
        ITypeCompletionContext completionContext,
        DefinitionBase? definition,
        IDictionary<string, object?> contextData)
    {
        if (definition is FilterInputTypeDefinition def)
        {
            IFilterConvention convention = GetConvention(
                completionContext.DescriptorContext,
                def.Scope);

            var descriptor = FilterInputTypeDescriptor.New(
                completionContext.DescriptorContext,
                def.EntityType,
                def.Scope);

            SchemaTypeReference typeReference = TypeReference.Create(
                completionContext.Type,
                def.Scope);

            convention.ApplyConfigurations(typeReference, descriptor);

            DataTypeExtensionHelper.MergeFilterInputTypeDefinitions(
                completionContext,
                descriptor.CreateDefinition(),
                def);

            if (def.Scope is not null)
            {
                definition.Name = completionContext.Scope +
                    "_" +
                    definition.Name;
            }
        }
    }

    public override void OnBeforeCompleteType(
        ITypeCompletionContext completionContext,
        DefinitionBase? definition,
        IDictionary<string, object?> contextData)
    {
        if (definition is FilterInputTypeDefinition def)
        {
            IFilterConvention convention = GetConvention(
                completionContext.DescriptorContext,
                def.Scope);

            foreach (InputFieldDefinition field in def.Fields)
            {
                if (field is FilterFieldDefinition filterFieldDefinition)
                {
                    if (filterFieldDefinition.Type is null)
                    {
                        throw ThrowHelper.FilterInterceptor_OperationHasNoTypeSpecified(
                            def,
                            filterFieldDefinition);
                    }

                    if (filterFieldDefinition.Handler is null)
                    {
                        if (convention.TryGetHandler(
                            completionContext,
                            def,
                            filterFieldDefinition,
                            out IFilterFieldHandler? handler))
                        {
                            filterFieldDefinition.Handler = handler;
                        }
                        else
                        {
                            throw ThrowHelper.FilterInterceptor_NoHandlerFoundForField(
                                def,
                                filterFieldDefinition);
                        }
                    }
                }
            }
        }
    }

    private IFilterConvention GetConvention(
        IDescriptorContext context,
        string? scope)
    {
        if (!_conventions.TryGetValue(
            scope ?? "",
            out IFilterConvention? convention))
        {
            convention = context.GetFilterConvention(scope);
            _conventions[scope ?? ""] = convention;
        }

        return convention;
    }

    private static void ApplyCorrectScope(
        InputObjectTypeDefinition definition,
        ITypeDiscoveryContext discoveryContext)
    {
        foreach (InputFieldDefinition field in definition.Fields)
        {
            if (field is FilterFieldDefinition filterFieldDefinition &&
                field.Type is not null &&
                filterFieldDefinition.Type is not null)
            {
                if (discoveryContext.TryPredictTypeKind(
                        filterFieldDefinition.Type,
                        out TypeKind kind) &&
                    kind != TypeKind.Scalar && kind != TypeKind.Enum)
                {
                    field.Type = field.Type.With(scope: discoveryContext.Scope);
                }
            }
        }
    }
}
