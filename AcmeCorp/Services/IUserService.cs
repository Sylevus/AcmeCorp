using AcmeCorp.Models;

namespace AcmeCorp.Services
{
    public interface IUserService
    {
        ResponseModel<UserModel> Get(long UserId);
    }
}
