using AmdarisInternship.API.Dtos.Account;
using AmdarisInternship.API.Infrastructure.Models;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(UserForLoginDto userForLoginDto);

        Task<LoginResponse> Logout();

        Task<RegistrationResponse> Register(UserForRegisterDto userForRegisterDto);
    }
}
