using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using TravelMate.Models;
using TravelMate2.Services;
using TravelMateUI.Services;

public interface ICustomAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task MarkUserAsAuthenticated(User user);
    Task MarkUserAsLoggedOut();
}

public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
{
    private readonly IUserUIService _userService;
    private readonly LocalStorageService _localStorageService;

    public CustomAuthenticationStateProvider(IUserUIService userService, LocalStorageService localStorageService)
    {
        _userService = userService;
        _localStorageService = localStorageService;
    }

    public async Task MarkUserAsAuthenticated(User user)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.UserType.ToString())
        }, "apiauth");

        var userPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userPrincipal)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userJson = await _localStorageService.GetItemAsync("currentUser");

        if (string.IsNullOrEmpty(userJson))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var user = JsonSerializer.Deserialize<User>(userJson);
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.UserType.ToString())
        }, "apiauth");

        var userPrincipal = new ClaimsPrincipal(identity);
        return new AuthenticationState(userPrincipal);
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorageService.RemoveItemAsync("currentUser");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
}



