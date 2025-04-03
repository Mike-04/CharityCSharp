using System;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Charity.Repo
{
    public class JdbcUtils
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JdbcUtils> _logger;

        public JdbcUtils(IConfiguration configuration, ILogger<JdbcUtils> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private SQLiteConnection GetNewConnection()
        {
            _logger.LogTrace("Entering GetNewConnection");

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            _logger.LogInformation("Trying to connect to database ... {0}", connectionString);

            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(connectionString);
                con.Open();
            }
            catch (SQLiteException e)
            {
                _logger.LogError(e, "Error getting connection");
                Console.WriteLine("Error getting connection " + e);
            }
            if (con == null)
            {
                _logger.LogError("Failed to establish connection to database");
                throw new Exception("Failed to establish connection to database");
            }
            return con;
        }

        public SQLiteConnection GetConnection()
        {
            _logger.LogTrace("Entering GetConnection");
            SQLiteConnection connection = null;
            try
            {
                connection = GetNewConnection();
            }
            catch (SQLiteException e)
            {
                _logger.LogError(e, "Error DB");
                Console.WriteLine("Error DB " + e);
            }
            _logger.LogTrace("Exiting GetConnection");
            return connection;
        }
    }
}