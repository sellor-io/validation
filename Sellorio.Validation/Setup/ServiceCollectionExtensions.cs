﻿using Sellorio.Validation.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace Sellorio.Validation.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationService(this IServiceCollection services)
    {
        services.TryAddScoped<IValidationService, ValidationService>();
        return services;
    }

    public static IServiceCollection AddValidatorsInAssembly<TAssembly>(this IServiceCollection services)
    {
        var validatorInterfaceNamespace = typeof(IValidator<>).Namespace;

        var validatorTypes =
            typeof(TAssembly).Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterface(nameof(IValidator<object>))?.Namespace == validatorInterfaceNamespace)
                .ToList();

        foreach (var validatorType in validatorTypes)
        {
            var primaryInterfaceType = validatorType.GetInterfaces()[0];
            services.TryAddTransient(primaryInterfaceType, validatorType);
        }

        return services;
    }
}
