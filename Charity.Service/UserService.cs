using Charity.Domain;
using Charity.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Charity.Service
{
    public class UserService
    {
        private readonly UserIRepository _userRepository;

        public UserService(UserIRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public static string HashPassword(string username, string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedPassword = username + password; // Use username as salt
                byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public static bool CheckPassword(string username, string password, string hash)
        {
            string newHash = HashPassword(username, password);
            return newHash == hash;
        }

        public void AddUser(String username, String password)
        {
            //hash password
            password = HashPassword(username,password);
            _userRepository.Add(new User(username, password));
        }

        public bool CheckUser(String username, String password)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                return false;
            }
            return CheckPassword(username, password, user.PasswordHash);
        }

        public void DeleteUser(String username)
        {
            User user = _userRepository.FindByUsername(username);
            if (user != null)
            {
                _userRepository.Remove(user.Id);
            }
        }

        public void UpdateUser(String username, String password)
        {
            User user = _userRepository.FindByUsername(username);
            if (user != null)
            {
                password = HashPassword(username,password);
                _userRepository.Update(new User(user.Id, username, password));
            }
        }

        public User FindByUsername(String username)
        {
            return _userRepository.FindByUsername(username);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

    }
}
