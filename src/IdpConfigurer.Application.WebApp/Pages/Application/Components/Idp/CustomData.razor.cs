using IdpConfigurer.Business.ViewController;
using Microsoft.AspNetCore.Components;

namespace IdpConfigurer.Application.WebApp.Pages.Application.Components.Idp
{
    public partial class CustomData
    {
#nullable disable
        [Inject] private IdpViewController ViewController { get; set; }
#nullable enable
    }
}