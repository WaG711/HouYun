using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HouYun3.Models;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(string userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUserNameAsync(string userName);
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<IdentityResult> UpdateUserAsync(User user);
    Task<IdentityResult> DeleteUserAsync(User user);
    Task<bool> CheckPasswordAsync(User user, string password);

}
