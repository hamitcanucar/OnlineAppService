using Microsoft.AspNetCore.Mvc;
using OnlineAppService.Domain.Common;
using OnlineAppService.Domain.Entities;

namespace OnlineAppService.WebApi.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger _logger;

        protected BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected Guid GetUserIdFromToken()
        {
            Guid userId = Guid.Empty;
            try
            {
                if (!Guid.TryParse(User.FindFirst(JWTUser.ID).Value, out userId))
                {
                    _logger.LogWarning("Wrong Guid format in token ({0})", JWTUser.ID);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("Use {0}() method in Authorize methods!", nameof(GetUserIdFromToken));
            }
            return userId;
        }

        protected UserTypes GetUserTypeFromToken()
        {
            UserTypes userType = UserTypes.User;
            try
            {
                if (!Enum.TryParse<UserTypes>(User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value, out userType))
                {
                    _logger.LogWarning("Wrong UserType format in token (role)");
                }
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Use {0}() method in Authorize methods!", nameof(GetUserTypeFromToken));
            }
            return userType;
        }
    }
}
