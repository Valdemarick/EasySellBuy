using AutoMapper;
using Identity.WebApi.Application.Models;
using Identity.WebApi.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.WebApi.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;

    public AuthService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthService> logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        //var user = _mapper.Map<User>(loginModel);

        var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> RegisterAsync(RegisterModel registerModel)
    {
        var user = _mapper.Map<User>(registerModel);

        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }
}
