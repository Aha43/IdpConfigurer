using IdpConfigurer.Specification.Api;
using Microsoft.AspNetCore.Mvc;

namespace IdpConfigurer.WebApi.Controllers
{
    public class IdpController : ControllerBase
    {
        private readonly IIdpApi _idpApi;

        public IdpController(IIdpApi idpApi) => _idpApi = idpApi;
    }
}
