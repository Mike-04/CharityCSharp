using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Charity.Domain;

namespace Charity.Repo
{
    public class UserDBRepository : UserIRepository, IDisposable
    {
        private readonly SQLiteConnection _connection;
        private readonly ILogger<UserDBRepository> _logger;

        public UserDBRepository(JdbcUtils dbUtils, ILogger<UserDBRepository> logger)
        {
            _connection = dbUtils.GetConnection();
            _logger = logger;
        }

        public void Add(User entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("INSERT INTO [User] VALUES (@Id, @Username, @PasswordHash)", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", entity.Id.ToString());
                    preStmt.Parameters.AddWithValue("@Username", entity.Username);
                    preStmt.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                    preStmt.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
        }

        public void Remove(Guid id)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("DELETE FROM [User] WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", id.ToString());
                    preStmt.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
        }

        public void Update(User entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("UPDATE [User] SET Username=@Username, PasswordHash=@PasswordHash WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Username", entity.Username);
                    preStmt.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                    preStmt.Parameters.AddWithValue("@Id", entity.Id.ToString());
                    preStmt.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
        }

        public User Find(Guid id)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM [User] WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", id.ToString());
                    using (var result = preStmt.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            var username = result["Username"].ToString();
                            var passwordHash = result["PasswordHash"].ToString();
                            var user = new User(username, passwordHash) { Id = id };
                            return user;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM [User]", _connection))
                {
                    using (var result = preStmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            if (Guid.TryParse(result["Id"].ToString(), out var id))
                            {
                                var username = result["Username"].ToString();
                                var passwordHash = result["PasswordHash"].ToString();
                                var user = new User(username, passwordHash) { Id = id };
                                users.Add(user);
                            }
                            else
                            {
                                _logger.LogError("Unrecognized Guid format for Id: {0}", result["Id"]);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
            return users;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public User FindByUsername(string username)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM [User] WHERE Username=@Username", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Username", username);
                    using (var result = preStmt.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            var id = Guid.Parse(result["Id"].ToString());
                            var passwordHash = result["PasswordHash"].ToString();
                            var user = new User(username, passwordHash) { Id = id };
                            return user;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
            return null;
        }
    }
}