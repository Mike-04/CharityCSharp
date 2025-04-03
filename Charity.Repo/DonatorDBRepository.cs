using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Charity.Domain;

namespace Charity.Repo
{
    public class DonatorDBRepository : DonatorIRepository, IDisposable
    {
        private readonly SQLiteConnection _connection;
        private readonly ILogger<DonatorDBRepository> _logger;

        public DonatorDBRepository(JdbcUtils dbUtils, ILogger<DonatorDBRepository> logger)
        {
            _connection = dbUtils.GetConnection();
            _logger = logger;
        }

        public void Add(Donator entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("INSERT INTO Donator VALUES (@Id, @Nume, @Adresa, @NumarTelefon)", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", entity.Id.ToString());
                    preStmt.Parameters.AddWithValue("@Nume", entity.Nume);
                    preStmt.Parameters.AddWithValue("@Adresa", entity.Adresa);
                    preStmt.Parameters.AddWithValue("@NumarTelefon", entity.NumarTelefon);
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
                using (var preStmt = new SQLiteCommand("DELETE FROM Donator WHERE Id=@Id", _connection))
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

        public void Update(Donator entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("UPDATE Donator SET Nume=@Nume, Adresa=@Adresa, NumarTelefon=@NumarTelefon WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Nume", entity.Nume);
                    preStmt.Parameters.AddWithValue("@Adresa", entity.Adresa);
                    preStmt.Parameters.AddWithValue("@NumarTelefon", entity.NumarTelefon);
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

        public Donator Find(Guid id)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM Donator WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", id.ToString());
                    using (var result = preStmt.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            var nume = result["Nume"].ToString();
                            var adresa = result["Adresa"].ToString();
                            var numarTelefon = result["NumarTelefon"].ToString();
                            var donator = new Donator(nume, adresa, numarTelefon) { Id = id };
                            return donator;
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

        public IEnumerable<Donator> GetAll()
        {
            var donators = new List<Donator>();
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM Donator", _connection))
                {
                    using (var result = preStmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            if (Guid.TryParse(result["Id"].ToString(), out var id))
                            {
                                var nume = result["Nume"].ToString();
                                var adresa = result["Adresa"].ToString();
                                var numarTelefon = result["NumarTelefon"].ToString();
                                var donator = new Donator(nume, adresa, numarTelefon) { Id = id };
                                donators.Add(donator);
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
            return donators;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public IEnumerable<Donator> FindByName(string nume)
        {
            var donators = new List<Donator>();
            try
            {
                using (var preStmt = new SQLiteCommand("SELECT * FROM Donator WHERE Nume LIKE @Nume", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Nume", "%" + nume + "%");
                    using (var result = preStmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            if (Guid.TryParse(result["Id"].ToString(), out var id))
                            {
                                var donatorNume = result["Nume"]?.ToString() ?? string.Empty;
                                var adresa = result["Adresa"]?.ToString() ?? string.Empty;
                                var numarTelefon = result["NumarTelefon"]?.ToString() ?? string.Empty;
                                var donator = new Donator(donatorNume, adresa, numarTelefon) { Id = id };
                                donators.Add(donator);
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
            return donators;
        }
    }
}