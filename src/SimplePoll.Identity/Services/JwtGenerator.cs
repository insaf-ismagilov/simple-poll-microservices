using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimplePoll.Identity.Entities;
using SimplePoll.Identity.Models;
using SimplePoll.Identity.Options;

namespace SimplePoll.Identity.Services
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly JwtSettings _jwtSettings;

		public JwtGenerator(IOptions<JwtSettings> jwtSettings)
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
				_jwtSettings.Audience,
				claims,
				expires: DateTime.UtcNow.AddSeconds(_jwtSettings.LifetimeSeconds),
				signingCredentials: credentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

			return token;
		}

		private static Claim[] GetClaims(User user)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty)
			};

			return claims;
		}
	}
}