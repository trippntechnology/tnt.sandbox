using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCMovie.Models;
using System;
using System.Linq;

namespace MvcMovie.Models
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new MVCMovieContext(
					serviceProvider.GetRequiredService<DbContextOptions<MVCMovieContext>>()))
			{
				// Look for any movies.
				if (context.Accounts.Any())
				{
					return;   // DB has been seeded
				}

				context.Accounts.AddRange(
						 new Accounts
						 {
							 Enabled = false,
							 Name = "Account One",
							 PhoneNumber = "111-111-1111"
						 },

						 new Accounts
						 {
							 Enabled = true,
							 Name = "Account True",
							 PhoneNumber = "222-222-2222"
						 },

						 new Accounts
						 {
							 Enabled = false,
							 Name = "Account Three",
							 PhoneNumber = "333-333-3333"
						 },

						 new Accounts
						 {
							 Enabled = true,
							 Name = "Account Four",
							 PhoneNumber = "444-444-4444"
						 }
				);
				context.SaveChanges();
			}
		}
	}
}