namespace Auction.Api;

public static class ApiConfiguration
{
  public static IConfiguration? Configuration { get; set; }
  public static string ConnectionString { get; set; } = string.Empty;
  public static string CorsPolicyName = "segredinho";
}