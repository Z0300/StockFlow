using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Users;
using StockFlow.Domain.Entities.Users.ValueObjects;

namespace StockFlow.Application.Users.Register;

internal sealed class RegisterUserCommandHandler
    : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {

        var user = User.Create(
            command.FirstName,
            command.LastName,
           new Email(command.Email),
            _passwordHasher.Hash(command.Password));

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}
