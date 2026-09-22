using Microsoft.AspNetCore.Mvc;
using Configuration.Options;
using Microsoft.Extensions.Options;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        readonly ILogger<AdminController> _logger;
        readonly DbConnectionSetsOptions _dbSetOptions;
        readonly AesEncryptionOptions _aesOptions;
        readonly JwtOptions _jwtOptions;
        readonly VersionOptions _versionOptions;
        readonly IConfiguration _configuration;
        readonly MySecretOptions _myMessageOptions;

        // GET api/admin/Version
        [HttpGet]
        [ActionName("Version")]
        [ProducesResponseType(typeof(VersionOptions), 200)]
        public IActionResult Version()
        {
            try { return Ok(_versionOptions); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving version information");
                return BadRequest(ex.Message);
            }
        }

        // GET api/admin/MySecret
        [HttpGet]
        [ActionName("MySecret")]
        [ProducesResponseType(typeof(MySecretOptions), 200)]
        public IActionResult MySecret()
        {
            try { return Ok(_myMessageOptions); }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MySecret)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        public AdminController(
            ILogger<AdminController> logger,
            IConfiguration configuration,
            IOptions<DbConnectionSetsOptions> dbSetOptions,
            IOptions<AesEncryptionOptions> aesOptions,
            IOptions<JwtOptions> jwtOptions,
            IOptions<VersionOptions> versionOptions,
            IOptions<MySecretOptions> myMessageOptions)
        {
            _logger = logger;
            _configuration = configuration;
            _dbSetOptions = dbSetOptions.Value;
            _aesOptions = aesOptions.Value;
            _jwtOptions = jwtOptions.Value;
            _versionOptions = versionOptions.Value;
            _myMessageOptions = myMessageOptions.Value;
        }
    }
}