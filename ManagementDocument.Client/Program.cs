using ManagementDocument.Client;
using ManagementDocument.Client.Handlers;
using ManagementDocument.Client.Interfaces;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Refit;

// https://localhost:44340/api
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<TokenHandler>();

builder.Services.AddRefitClient<IDocumentData>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://localhost:44340/api");
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddRefitClient<IAuthData>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://localhost:44340/api");
});

builder.Services.AddRefitClient<IRegistrationData>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://localhost:44340/api");
});

await builder.Build().RunAsync();
