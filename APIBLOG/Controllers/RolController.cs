using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRoles()
        {
            try
            {
                var roles =await _rolService.Get();
                if (roles.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(roles);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error En el servicio", ex);
            }
        }
    }
}
