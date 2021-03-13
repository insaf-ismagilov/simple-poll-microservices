using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Entities;
using SimplePoll.Identity.Models;
using SimplePoll.Identity.Models.Requests;
using SimplePoll.Identity.Queries;

namespace SimplePoll.Identity.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IJwtGenerator _jwtGenerator;

		public IdentityService(
			IMapper mapper,
			IMediator mediator,
			IPasswordHasher<User> passwordHasher,
			IJwtGenerator jwtGenerator)
		{
			_mapper = mapper;
			_mediator = mediator;
			_passwordHasher = passwordHasher;
			_jwtGenerator = jwtGenerator;
		}
		
		public async Task<ServiceResponse<AuthenticationResult>> SignInAsync(SignInRequest request)
		{
			var query = _mapper.Map<GetUserByEmailQuery>(request);

			var user = await _mediator.Send(query);
			
			if (user == null || !VerifyHPassword(user, request.Password))
				return ServiceResponse<AuthenticationResult>.Error();
			
			var token = _jwtGenerator.GetToken(user);

			return ServiceResponse<AuthenticationResult>.Success(new AuthenticationResult
			{
				AccessToken = token
			});
		}

		public Task<ServiceResponse> SignUpAsync(SignUpRequest request)
		{
			throw new NotImplementedException();
		}
		
		private bool VerifyHPassword(User user, string providedPassword)
		{
			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
			return result == PasswordVerificationResult.Success;
		}
	}
}