using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace OAuth2Server
{
	static class Clients
	{
		public static List<Client> Get()
		{
			return new List<Client>
						{
                // no human involved
                new Client
								{
										ClientName = "Silicon-only Client",
										ClientId = "silicon",
										Enabled = true,
										AccessTokenType = AccessTokenType.Reference,

										Flow = Flows.ClientCredentials,

										ClientSecrets = new List<Secret>
										{
												new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
										},

										AllowedScopes = new List<string>
										{
												"api1"
										}
								},
								new Client
								{
									ClientName = "WIN",
									ClientId = "367af82b-5c8e-47f9-b881-c84401632655",
									Enabled = true,
									//AccessTokenType = AccessTokenType.Reference,
									Flow = Flows.AuthorizationCode,
									//ClientSecrets = new List<Secret>{new Secret("".Sha256())},
									AllowedScopes = new List<string>{"api1"},
									RedirectUris = new List<string> {"https://hisp1.com"}
								},

                // human is involved
                new Client
								{
										ClientName = "Silicon on behalf of Carbon Client",
										ClientId = "carbon",
										Enabled = true,
										AccessTokenType = AccessTokenType.Reference,

										Flow = Flows.ResourceOwner,

										ClientSecrets = new List<Secret>
										{
												new Secret("21B5F798-BE55-42BC-8AA8-0025B903DC3B".Sha256())
										},

										AllowedScopes = new List<string>
										{
												"api1"
										}
								}
						};
		}
	}
}
