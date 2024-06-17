using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smooth.Client.Application.Hubs;
using Smooth.Client.Application.Managers;
using Smooth.Flaunt;
using Smooth.Flaunt.Configuration;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddHttpClients();
//builder.AddMsalAuthentication();

//builder.Services.AddScoped<ApiAuthorizationMessageHandler>();
builder.Services.AddScoped<IHttpDataManager, HttpDataManager>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<NotificationsHubService>();
builder.Services.AddScoped<ProgressHubService>();

await builder.Build().RunAsync();
