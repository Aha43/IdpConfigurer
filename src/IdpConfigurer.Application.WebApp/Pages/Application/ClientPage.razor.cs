using IdpConfigurer.Business.ViewController;
using IdpConfigurer.Business.ViewModel;
using IdpConfigurer.Domain;
using Microsoft.AspNetCore.Components;

namespace IdpConfigurer.Application.WebApp.Pages.Application;

public partial class ClientPage
{
    [Parameter] public string? IdpName { get; set; }
    [Parameter] public string? ClientId { get; set; }

#nullable disable
    [Inject] private ClientViewController ViewController { get; set; }
#nullable enable

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrWhiteSpace(IdpName) && !string.IsNullOrWhiteSpace(ClientId))
        {
            await ViewController.LoadAsync(IdpName, ClientId).ConfigureAwait(true);
        }
    }

}
