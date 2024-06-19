using Service_1.APIs.Common;
using Service_1.APIs.Dtos;

namespace Service_1.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<UserDto> CreateUser(UserCreateInput userDto);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserIdDto idDto);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<UserDto>> Users(UserFindMany findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<UserDto> User(UserIdDto idDto);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserIdDto idDto, UserUpdateInput updateDto);

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindMany findManyArgs);
}
