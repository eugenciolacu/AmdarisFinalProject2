using AmdarisInternship.API.Dtos.Account;
using AmdarisInternship.API.Dtos.UserDtos;
using AmdarisInternship.API.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(UserForLoginDto userForLoginDto);

        Task<LoginResponse> Logout();

        Task<RegistrationResponse> Register(UserForRegisterDto userForRegisterDto);

        Task<IList<LecturerDto>> GetLecturers();
    }
}
