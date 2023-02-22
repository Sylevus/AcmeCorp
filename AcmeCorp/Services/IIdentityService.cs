using AcmeCorp.Models;

namespace AcmeCorp.Services
{
    public interface IIdentityService
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login);
        Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request);
    }
}
