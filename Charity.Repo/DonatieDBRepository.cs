using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Charity.Domain;

namespace Charity.Repo
{
    public class DonatieDBRepository : DonatieIRepository, IDisposable
    {
        private readonly SQLiteConnection _connection;
        private readonly ILogger<DonatieDBRepository> _logger;

        public DonatieDBRepository(JdbcUtils dbUtils, ILogger<DonatieDBRepository> logger)
        {
            _connection = dbUtils.GetConnection();
            _logger = logger;
        }

        public void Add(Donatie entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("INSERT INTO Donatie VALUES (@Id, @DonatorId, @CazCaritabilId, @SumaDonata, @Timestamp)", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", entity.Id.ToString());
                    preStmt.Parameters.AddWithValue("@DonatorId", entity.Donator.Id.ToString());
                    preStmt.Parameters.AddWithValue("@CazCaritabilId", entity.CazCaritabil.Id.ToString());
                    preStmt.Parameters.AddWithValue("@SumaDonata", entity.SumaDonata);
                    preStmt.Parameters.AddWithValue("@Timestamp", entity.Timestamp);
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
                using (var preStmt = new SQLiteCommand("DELETE FROM Donatie WHERE Id=@Id", _connection))
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

        public void Update(Donatie entity)
        {
            try
            {
                using (var preStmt = new SQLiteCommand("UPDATE Donatie SET DonatorId=@DonatorId, CazCaritabilId=@CazCaritabilId, SumaDonata=@SumaDonata, Timestamp=@Timestamp WHERE Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@DonatorId", entity.Donator.Id.ToString());
                    preStmt.Parameters.AddWithValue("@CazCaritabilId", entity.CazCaritabil.Id.ToString());
                    preStmt.Parameters.AddWithValue("@SumaDonata", entity.SumaDonata);
                    preStmt.Parameters.AddWithValue("@Timestamp", entity.Timestamp);
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

        public Donatie Find(Guid id)
        {
            try
            {
                using (var preStmt = new SQLiteCommand(
                    "SELECT d.Id, d.SumaDonata, d.Timestamp, " +
                    "don.Id as DonatorId, don.Nume as DonatorNume, don.Adresa as DonatorAdresa, don.NumarTelefon as DonatorNumarTelefon, " +
                    "caz.Id as CazId, caz.Nume as CazNume, caz.SumaAdunata as CazSumaAdunata " +
                    "FROM Donatie d " +
                    "JOIN Donator don ON d.DonatorId = don.Id " +
                    "JOIN CazCaritabil caz ON d.CazCaritabilId = caz.Id " +
                    "WHERE d.Id=@Id", _connection))
                {
                    preStmt.Parameters.AddWithValue("@Id", id.ToString());
                    using (var result = preStmt.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            if (Guid.TryParse(result["DonatorId"].ToString(), out var donatorId) &&
                                Guid.TryParse(result["CazId"].ToString(), out var cazId))
                            {
                                var donator = new Donator(result["DonatorNume"].ToString(), result["DonatorAdresa"].ToString(), result["DonatorNumarTelefon"].ToString())
                                {
                                    Id = donatorId
                                };
                                var cazCaritabil = new CazCaritabil(result["CazNume"].ToString(), Convert.ToDouble(result["CazSumaAdunata"]))
                                {
                                    Id = cazId
                                };
                                var donatie = new Donatie(donator, cazCaritabil, Convert.ToDouble(result["SumaDonata"]))
                                {
                                    Id = id,
                                    Timestamp = Convert.ToDateTime(result["Timestamp"])
                                };
                                return donatie;
                            }
                            else
                            {
                                _logger.LogError("Unrecognized Guid format for DonatorId or CazId");
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
            return null;
        }

        public IEnumerable<Donatie> GetAll()
        {
            var donatii = new List<Donatie>();
            try
            {
                using (var preStmt = new SQLiteCommand(
                    "SELECT d.Id, d.SumaDonata, d.Timestamp, " +
                    "don.Id as DonatorId, don.Nume as DonatorNume, don.Adresa as DonatorAdresa, don.NumarTelefon as DonatorNumarTelefon, " +
                    "caz.Id as CazId, caz.Nume as CazNume, caz.SumaAdunata as CazSumaAdunata " +
                    "FROM Donatie d " +
                    "JOIN Donator don ON d.DonatorId = don.Id " +
                    "JOIN CazCaritabil caz ON d.CazCaritabilId = caz.Id", _connection))
                {
                    using (var result = preStmt.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            if (Guid.TryParse(result["DonatorId"].ToString(), out var donatorId) &&
                                Guid.TryParse(result["CazId"].ToString(), out var cazId) &&
                                Guid.TryParse(result["Id"].ToString(), out var id))
                            {
                                var donator = new Donator(result["DonatorNume"].ToString(), result["DonatorAdresa"].ToString(), result["DonatorNumarTelefon"].ToString())
                                {
                                    Id = donatorId
                                };
                                var cazCaritabil = new CazCaritabil(result["CazNume"].ToString(), Convert.ToDouble(result["CazSumaAdunata"]))
                                {
                                    Id = cazId
                                };
                                var donatie = new Donatie(donator, cazCaritabil, Convert.ToDouble(result["SumaDonata"]))
                                {
                                    Id = id,
                                    Timestamp = Convert.ToDateTime(result["Timestamp"])
                                };
                                donatii.Add(donatie);
                            }
                            else
                            {
                                _logger.LogError("Unrecognized Guid format for DonatorId, CazId, or Id");
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
            return donatii;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}