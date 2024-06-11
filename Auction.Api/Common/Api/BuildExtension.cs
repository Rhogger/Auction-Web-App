using Auction.Api.Data;
using Auction.Api.Handlers;
using Auction.Core;
using Auction.Core.Handlers;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Auction.Api.Common.Api;

public static class BuildExtension
{
  public static async void AddKeyVault(this WebApplicationBuilder builder)

  {
    string secretName = builder.Configuration["KeyVaultConfig:SecretName"];
    string kvUri = builder.Configuration["KeyVaultConfig:KVUri"];

    var credential = new DefaultAzureCredential();
    var client = new SecretClient(new Uri(kvUri), credential);

    var secret = await client.GetSecretAsync(secretName);

    ApiConfiguration.ConnectionString = secret.Value.Value ?? string.Empty;

    Console.WriteLine(ApiConfiguration.ConnectionString);
  }

  public static void AddConfiguration(this WebApplicationBuilder builder)
  {
    Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
    Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
  }

  public static void AddDocumentation(this WebApplicationBuilder builder)
  {
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(x =>
    {
      x.CustomSchemaIds(n => n.FullName);
    });
  }

  public static void AddDataContext(this WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(ApiConfiguration.ConnectionString));
  }

  public static void AddCrossOrigin(this WebApplicationBuilder builder)
  {
    builder
    .Services
    .AddCors(options => options
                        .AddPolicy(ApiConfiguration
                                  .CorsPolicyName, policy => policy
                                                            .WithOrigins([Configuration.BackendUrl,
                                                                          Configuration.FrontendUrl])
                                                            .AllowAnyMethod()
                                                            .AllowAnyHeader()
                                                            .AllowCredentials()));
  }

  public static void AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddTransient<IBidderHandler, BidderHandler>();
    builder.Services.AddTransient<IBidHandler, BidHandler>();
    builder.Services.AddTransient<IItemHandler, ItemHandler>();
  }
}