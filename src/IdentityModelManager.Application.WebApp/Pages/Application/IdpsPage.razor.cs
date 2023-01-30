using IdentityModelManager.Business.ViewController;
using Microsoft.AspNetCore.Components;

namespace IdentityModelManager.Application.WebApp.Pages.Application
{
    public partial class IdpsPage
    {
#nullable disable
        [Inject] private IdpsViewController ViewController { get; set; }
#nullable enable

        protected override async Task OnInitializedAsync() => await ViewController.LoadAsync().ConfigureAwait(true);

    }
}