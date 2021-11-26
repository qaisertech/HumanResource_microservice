using IdentityServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityServer()
   .AddInMemoryClients(Config.Clients)
 .AddInMemoryApiScopes(Config.ApiScopes)
 .AddDeveloperSigningCredential();
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseRouting();
app.UseIdentityServer();
app.Run();
