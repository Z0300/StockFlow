using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Users.GetUserById;

namespace StockFlow.Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId:guid}", async (
                Guid userId,
                IQueryHandler<GetUserByIdQuery, UserResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(userId);

                Result<UserResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            //.HasPermission(Permissions.UsersAccess)
            .RequireAuthorization() 
            .WithTags(Tags.Users);
    }
}
