using IndentityLogic.DTOs;

namespace IndentityLogic.Interfaces
{
    public interface IRegister
    {
        public Task<AccountResult<UserDto, IEnumerable<string>>> RegisterHandlerAsync(RegisterDto registerDto);
    }
}
