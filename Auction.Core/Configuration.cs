namespace Auction.Core;

public static class Configuration
{
  public const int DefaultPageSize = 12;
  public const int DefaultPageNumber = 1;
  public const int DefaultCurrentPage = 1;

  public const int DefaultStatusCode = 200;

  public static string BackendUrl { get; set; } = string.Empty;
  public static string FrontendUrl { get; set; } = string.Empty;
}