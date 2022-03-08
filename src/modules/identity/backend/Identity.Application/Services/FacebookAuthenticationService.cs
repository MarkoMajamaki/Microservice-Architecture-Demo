using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.Application;

public interface IFacebookAuthenticationService : IExternalAuthenticationService {}

public class FacebookAuthenticationService : IFacebookAuthenticationService
{
    private const string _tokenValidationUrl = 
        "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
    private const string _userInfoUrl = 
        "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";

    private FacebookAuthSettings _facebookAuthSettings;
    public IHttpClientFactory _httpClientFactory;

    public FacebookAuthenticationService(
        IOptions<FacebookAuthSettings> facebookAuthSettings,
        IHttpClientFactory httpClientFactory)
    {
        _facebookAuthSettings = facebookAuthSettings.Value;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<UserInfo> AuthenticateAsync(string accessToken)
    {
        FacebookTokenValidationResponse validationResponse = await ValidateAccessTokenAsync(accessToken);

        if (!validationResponse.Data.IsValid)
        {
            throw new InvalidTokenException();
        }

        var userInfo = await GetUserInfoAsync(accessToken);

        return new UserInfo(userInfo.Email, $"{userInfo.LastName} {userInfo.FirstName}");
    }

    private async Task<FacebookTokenValidationResponse> ValidateAccessTokenAsync(string accessToken)
    {
        var fromattedUrl = string.Format(_tokenValidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);
        var result = await _httpClientFactory.CreateClient().GetAsync(fromattedUrl);
        result.EnsureSuccessStatusCode();
        var resposeAsString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FacebookTokenValidationResponse>(resposeAsString);
    }

    private async Task<FacebookUserInfoResponse> GetUserInfoAsync(string accessToken)
    {
        var fromattedUrl = string.Format(_userInfoUrl, accessToken);
        var result = await _httpClientFactory.CreateClient().GetAsync(fromattedUrl);
        result.EnsureSuccessStatusCode();
        var resposeAsString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FacebookUserInfoResponse>(resposeAsString);
    }
}