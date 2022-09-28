using FluentValidation;

namespace Ad.Application.Ads.Queries.GetAd;

public class GetAdQueryValidator : AbstractValidator<GetAdQuery>
{
    public GetAdQueryValidator()
    {
        RuleFor(m => m.Id).NotEmpty();
    }
}