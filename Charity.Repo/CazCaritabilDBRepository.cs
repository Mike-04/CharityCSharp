using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Charity.Domain;

namespace Charity.Repo
{
    public class CazCaritabilDBRepository : CazCaritabilIRepository, IDisposable
    {
        private readonly SQLiteConnection _connection;
        private readonly ILogger<CazCaritabilDBRepository> _logger;

        public CazCaritabilDBRepository(JdbcUtils dbUtils, ILogger<CazCaritabilDBRepository> logger)
        {
            _connection = dbUtils.GetConnection();
            _logger = logger;
        }

        public void Add(CazCaritabil entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("INSERT INTO CazCaritabil VALUES (@Id, @Nume, @SumaAdunata)", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", entity.Id.ToString());
                    preStmt.Parameters.AddWithValue("@Nume", entity.Nume);
                    preStmt.Parameters.AddWithValue("@SumaAdunata", entity.SumaAdunata);
                    preStmt.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError(ex, "Error DB");
                Console.Error.WriteLine("Error DB " + ex);
            }
        }

        public void Remove(CazCaritabil entity)
        {
            Remove(entity.Id);
        }

        public void Remove(Guid id)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("DELETE FROM CazCaritabil WHERE Id=@Id", _connection))
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

        public void Update(CazCaritabil entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("UPDATE CazCaritabil SET Nume=@Nume, SumaAdunata=@SumaAdunata WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Nume", entity.Nume);
                    preStmt.Parameters.AddWithValue("@SumaAdunata", entity.SumaAdunata);
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

        public CazCaritabil Find(Guid id)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM CazCaritabil WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", id.ToString());
                    using (var result = preStmt.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            var nume = result["Nume"].ToString();
                            var sumaAdunata = Convert.ToDouble(result["SumaAdunata"]);
                            var cazCaritabil = new CazCaritabil(nume, sumaAdunata) { Id = id };
                            return cazCaritabil;
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

        public IEnumerable<CazCaritabil> GetAll()
        {
            var cazuriCaritabile = new List<CazCaritabil>();
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM CazCaritabil", _connection))
                {
                    using (var result = preStmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            if (Guid.TryParse(result["Id"].ToString(), out var id))
                            {
                                var nume = result["Nume"].ToString();
                                var sumaAdunata = Convert.ToDouble(result["SumaAdunata"]);
                                var cazCaritabil = new CazCaritabil(nume, sumaAdunata) { Id = id };
                                cazuriCaritabile.Add(cazCaritabil);
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
            return cazuriCaritabile;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}