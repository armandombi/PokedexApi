using Microsoft.AspNetCore.Mvc;

namespace PokedexApi.Controllers.V1
{
    /// <summary>
    ///     Controller to handle all erros
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handler to capture errors and show a user appropriate message
        /// </summary>
        /// <returns></returns>
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
