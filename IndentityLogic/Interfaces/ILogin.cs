using IndentityLogic.DTOs;

namespace IndentityLogic.Interfaces
{
    public interface ILogin
    {
        public Task<UserDto> LoginHandleAsync(LoginDto loginDto);
    }
}
