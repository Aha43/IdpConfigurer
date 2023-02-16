using IdpConfigurer.Business.ViewController;
using Microsoft.AspNetCore.Components;

namespace IdpConfigurer.Application.WebApp.Pages.Application.Components.Client
{
    public partial class RedirectUris
    {
#nullable disable
        [Inject] private ClientViewController ViewController { get; set; }
#nullable enable
    }
}