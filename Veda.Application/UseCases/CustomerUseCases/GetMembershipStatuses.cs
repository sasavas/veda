using MediatR;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Ports.DataAccess;

namespace Veda.Application.UseCases.CustomerUseCases;

public record GetMembershipStatusesRequest() : IRequest<IEnumerable<MembershipStatus>>;

public class GetMembershipStatusesRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetMembershipStatusesRequest, IEnumerable<MembershipStatus>>
{
    public Task<IEnumerable<MembershipStatus>> Handle(
        GetMembershipStatusesRequest request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            unitOfWork.GetRepository<MembershipStatus>().GetAll()
        );
    }
}