using System;
using CleanUpMyMails.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanUpMyMails
{
	class Program
	{
		static void Main(string[] args)
		{
			var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

			var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
								devEnvironmentVariable.ToLower() == "development";
			//Determines the working environment as IHostingEnvironment is unavailable in a console app

			var builder = new ConfigurationBuilder();
			// tell the builder to look for the appsettings.json file
			builder
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			//only add secrets in development
			if(isDevelopment)
			{
				builder.AddUserSecrets<Program>();
			}

			using(
				var connection = new ImapConnection(
					builder.Build().Get<ImapConfiguration>()
				)
			)
			{
				connection.Connect();

				foreach(var email in new LatestEmails(connection, 10))
				{
					Console.WriteLine($"Email from: {email.From}");
					Console.WriteLine($"Subject: {email.Subject}");
				}

				Console.ReadKey();
			}
		}
	}
}
