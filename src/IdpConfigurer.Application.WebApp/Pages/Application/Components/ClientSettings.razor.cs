using IdpConfigurer.Business.ViewController;
using Microsoft.AspNetCore.Components;

namespace IdpConfigurer.Application.WebApp.Pages.Application.Components
{
    public partial class ClientSettings 
    {
#nullable disable
        [Inject] private ClientViewController ViewController { get; set; }
#nullable enable
    }
}