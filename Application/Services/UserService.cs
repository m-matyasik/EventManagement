using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with ID {id} not found.");
        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        return user ?? null;
    }

    public async Task CreateUserAsync(User user) => await _userRepository.AddUserAsync(user);

    public async Task UpdateUserAsync(User user)
    {
        var toUpdate = await _userRepository.GetUserByIdAsync(user.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"User with ID {user.Id} not found.");
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var toDelete = await _userRepository.GetUserByIdAsync(id);
        if (toDelete == null) throw new KeyNotFoundException($"User with ID {id} not found.");
        await _userRepository.DeleteUserAsync(id);
    }
}