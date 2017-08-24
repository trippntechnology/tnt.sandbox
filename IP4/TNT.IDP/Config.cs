using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TNT.IDP
{
	public static class Config
	{
		public static List<TestUser> GetUsers()
		{
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "48636249-207B-4272-836A-E9799C973BC4",
					Username = "Frank",
					Password = "password",
					Claims = new List<Claim>
					{
						new Claim("given_name", "Frank"),
						new Claim("family_name", "Underwood"),
					}
				},
				new TestUser
				{
					SubjectId = "8B4A28BC-19DC-4717-8B72-95C828F76D2E",
					Username = "Claire",
					Password = "password",
					Claims = new List<Claim>
					{
						new Claim("given_name", "Claire"),
						new Claim("family_name", "Underwood"),
					}
				}
			};
		}

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>();
		}
	}
}
