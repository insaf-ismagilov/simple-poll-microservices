using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimplePoll.Identity.Application.Options;
using SimplePoll.Identity.Domain.Entities;

namespace SimplePoll.Identity.Application.Services
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly IdentityJwtSettings _jwtSettings;

		public JwtGenerator(IOptions<IdentityJwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		public string GetToken(User user)
		{
			if (user == null)
				return null;

			var claims = GetClaims(user);
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				_jwtSettings.Issuer,
				claims: claims,
				expires: DateTime.UtcNow.AddSeconds(_jwtSettings.LifetimeSeconds),
				signingCredentials: credentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

			return token;
		}

		private IEnumerable<Claim> GetClaims(User user)
		{
			var claims = new List<Claim>
			{
				new(ClaimTypes.Email, user.Email),
				new(ClaimTypes.Role, user.Role?.Name ?? string.Empty)
			};

			claims.AddRange(_jwtSettings.Audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));

			return claims;
		}
	}
}