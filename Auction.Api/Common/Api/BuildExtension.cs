using Auction.Api.Data;
using Auction.Api.Handlers;
using Auction.Core;
using Auction.Core.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Common.Api;

public static class BuildExtension
{
  public static void AddConfiguration(this WebApplicationBuilder builder)
  {
    ApiConfiguration.Configuration = new ConfigurationBuilder().AddEnvironmentVariables("Default").Build();

    ApiConfiguration.ConnectionString = ApiConfiguration.Configuration.GetConnectionString("Default") ?? string.Empty;
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