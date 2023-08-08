using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;


namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().NotNull().WithMessage("Please enter the product name").MaximumLength(150).MinimumLength(2)
                .WithMessage("Add minimum 2 character and maximum 150 character");

            RuleFor(p => p.Stock)
                .NotEmpty().NotNull().WithMessage("Please enter product stock information").Must(s => s >= 0)
                .WithMessage("Enter positive number");

            RuleFor(p => p.Price)
                .NotNull().NotEmpty().WithMessage("Please enter products price").Must(s => s >= 0)
                .WithMessage("Enter positive number");
        }

    }
}
