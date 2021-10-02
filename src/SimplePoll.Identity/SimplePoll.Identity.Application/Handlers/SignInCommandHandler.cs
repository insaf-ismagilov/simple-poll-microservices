using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Application.Commands;
using SimplePoll.Identity.Application.Models;
using SimplePoll.Identity.Application.Services;
using SimplePoll.Identity.Domain.Entities;
using SimplePoll.Identity.Domain.Repositories;

namespace SimplePoll.Identity.Application.Handlers
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, ServiceResponse<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;

        public SignInCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }
        
        public async Task<ServiceResponse<AuthenticationResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            
            if (user == null || !VerifyHPassword(user, request.Password))
                return ServiceResponse<AuthenticationResult>.Error();
			
            var token = _jwtGenerator.GetToken(user);

            return ServiceResponse<AuthenticationResult>.Success(new AuthenticationResult
            {
                AccessToken = token
            });
        }
        
        private bool VerifyHPassword(User user, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}