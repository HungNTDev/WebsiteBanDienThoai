using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorAppCustomer;
using BlazorAppCustomer.Service.CategoryService;
using BlazorAppCustomer.Service.ImageService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7026/api/") // Đổi thành địa chỉ API của bạn
});
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ImageService>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
