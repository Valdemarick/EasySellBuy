using Identity.WebApi.Application.Models;

namespace Identity.WebApi.Application.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginModel loginModel);
    Task<bool> RegisterAsync(RegisterModel registerModel);
}