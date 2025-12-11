using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Users;

namespace StockFlow.Application.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());
        }

        UserResponse? user = await context.Users
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .SingleOrDefaultAsync(cancellationToken);

        return user ?? Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
    }
}
