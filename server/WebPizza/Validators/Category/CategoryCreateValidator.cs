using FluentValidation;
using WebPizza.Services.Interfaces;
using WebPizza.ViewModels.Category;

namespace WebPizza.Validators.Category;
public class CategoryCreateValidator : AbstractValidator<CategoryCreateVm>
{
    public CategoryCreateValidator(IImageValidator imageValidator)
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage("Name is empty or null")
            .MaximumLength(100)
                .WithMessage("Name is too long");

        RuleFor(c => c.Image)
            .NotNull()
                .WithMessage("Image is not selected")
            .DependentRules(() =>
            {
                RuleFor(c => c.Image)
                    .MustAsync(imageValidator.IsValidImageAsync)
                    .WithMessage("Image is not valid");
            });
    }
}