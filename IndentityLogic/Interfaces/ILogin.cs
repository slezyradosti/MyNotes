using IndentityLogic.Core;
using IndentityLogic.DTOs;

namespace IndentityLogic.Interfaces
{
    public interface ILogin
    {
        public Task<AccountResult<UserDto, string>> LoginHandleAsync(LoginDto loginDto);
    }
}
