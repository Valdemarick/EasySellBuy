using MediatR;

namespace Ad.Application.Ads.Commands.Delete;

public class DeleteAdCommand : IRequest
{
    public int Id { get; set; }
}