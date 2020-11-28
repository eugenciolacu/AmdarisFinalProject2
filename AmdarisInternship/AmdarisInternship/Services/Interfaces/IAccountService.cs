using AmdarisInternship.API.Dtos.Account;
using AmdarisInternship.API.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<JwtSecurityToken> Login(UserForLoginDto userForLoginDto);

        Task<RegistrationResponse> Register(UserForRegisterDto userForRegisterDto);
    }
}
