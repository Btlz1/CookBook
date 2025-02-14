using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions;

public interface IUserRepository 
{
    Task<UsersVm> GetUsers();
    Task<UsersVm?> GetUserById(int id);
    Task<User> AddUser(CreateUserDto dto);
    Task<int> UpdateUser(int userId, UpdateUserDto dto);
    Task DeleteUser(int id);
    Task<User> LoginUser(string login, string password);
    User TryGetUserByIdAndThrowIfNotFound(int id);
}