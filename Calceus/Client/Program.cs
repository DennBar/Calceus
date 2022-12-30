global using System.Net.Http.Json;
global using Calceus.Shared;
global using Calceus.Client.Services.AuthService;
global using Calceus.Client.Services.RoleService;
using Microsoft.AspNetCore.Components.Web;
using Calceus.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();


await builder.Build().RunAsync();
