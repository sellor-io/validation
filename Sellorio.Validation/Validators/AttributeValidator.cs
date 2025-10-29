﻿using System.Threading.Tasks;

namespace Sellorio.Validation.Validators;

public abstract class AttributeValidator<TObject> : IValidator<TObject>
{
    public Task ValidateAsync(IValidationBuilder<TObject> validate)
    {
        validate.Attributes();
        return Task.CompletedTask;
    }
}
