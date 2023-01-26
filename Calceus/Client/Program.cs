global using System.Net.Http.Json;
global using Calceus.Shared;
global using Microsoft.AspNetCore.Components.Authorization;
global using Calceus.Client.Services.RoleService;
global using Calceus.Client.Services.CategoryService;
global using Calceus.Client.Services.ColorService;
global using Calceus.Client.Services.SizeService;
global using Calceus.Client.Services.ProductService;
global using Calceus.Client.Services.StoreService;
global using Calceus.Client.Services.AuthService;
using Microsoft.AspNetCore.Components.Web;
using Calceus.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IStoreService,StoreService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();
