using FluentValidation;
using ImaginaryRealEstate.Models.Offers;

namespace ImaginaryRealEstate.Validators.Offer;

public class NewOfferIncomingDtoValidator: AbstractValidator<NewOfferIncomingDto>
{
    public NewOfferIncomingDtoValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty();

        RuleFor(x => x.Area)
            .NotNull();

        RuleFor(x => x.Bathrooms)
            .NotNull();

        RuleFor(x => x.Bedrooms)
            .NotNull();

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(10240);

        RuleFor(x => x.Price)
            .NotNull();

        RuleFor(x => x.Title)
            .NotNull()
            .MaximumLength(2048);
    }
}