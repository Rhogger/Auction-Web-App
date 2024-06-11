using Auction.Api;
using Auction.Api.Common.Api;
using Auction.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddKeyVault();
builder.AddConfiguration();
builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();
