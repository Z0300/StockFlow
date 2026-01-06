using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Users;
using StockFlow.Application.Users.GetLoggedInUser;
using StockFlow.Application.Users.Login;
using StockFlow.Application.Users.LoginWithRefreshToken;
using StockFlow.Application.Users.Register;
using StockFlow.Application.Users.RevokeRefreshToken;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Infrastructure.Authorization;

namespace StockFlow.Api.Endpoints.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [HasPermission(Permissions.AdminAccess)]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        Result<TokenResponse> result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginWithRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginWithRefreshTokenCommand(request.RefreshToken);

        Result<TokenResponse> result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{userId:guid}/refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken(Guid userId, CancellationToken cancellationToken)
    {
        var command = new RevokeRefreshTokenCommand(userId);

        Result<bool> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HttpGet("me")]
    [HasPermission(Permissions.UsersAccess)]
    public async Task<IActionResult> GetLoggedInUserV2(CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery();

        Result<UserResponse> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
