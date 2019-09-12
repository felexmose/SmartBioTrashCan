using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSmartTrashService.Model;

namespace RestSmartTrashService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class weightController : ControllerBase
    {
        private static string weightUri = "https://restsmarttrashservice.azurewebsites.net/api/weight";

        public static async Task<IList<weight>> GetWeightAsync(int id)
        {
            string requestUri = weightUri + "/" + id;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(requestUri);
                IList<weight> c = JsonConvert.DeserializeObject<IList<weight>>(content);
                
                return c;
            }
        }
        string connectionString = "Server=tcp:projektgruppe.database.windows.net,1433;Initial Catalog=smartbiotrashcanDB;Persist Security Info=False;User ID=projektGruppe;Password=1234ABcd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/weight
        [HttpGet]
        public void GetAllWeights()
        {
            //const string selectString = "select id, date, weight from weight order by id";
            //using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            //{
            //    databaseConnection.Open();
            //    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
            //    {
            //        using (SqlDataReader reader = selectCommand.ExecuteReader())
            //        {
            //            List<weight> weightList = new List<weight>();
            //            while (reader.Read())
            //            {
            //                weight weight = ReadWeight(reader);
            //                weightList.Add(weight);
            //            }
            //            return weightList;
            //        }
            //    }
            //}
        }

        private static weight ReadWeight(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string dato = reader.GetString(1);
            string weightMeasure = reader.GetString(2);

            weight weight = new weight
            {
                id = id,
                dato = dato,
                weightMeasure= weightMeasure
            };
            return weight;
        }

        // GET: api/weight/5
        [HttpGet("{id}", Name = "Get1")]
        public IEnumerable<weight> Get(int id)
        {
            if (id == 1)
            {
                const string selectString = "SELECT top 1 * FROM Weight order by id DESC";
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            List<weight> weightList = new List<weight>();
                            while (reader.Read())
                            {
                                weight weight = ReadWeight(reader);
                                weightList.Add(weight);
                            }
                            return weightList;
                        }
                    }
                }
            }
            if (id == 2)
            {
                const string selectString = "SELECT top 7 * FROM Weight order by id DESC";
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            List<weight> weightList = new List<weight>();
                            while (reader.Read())
                            {
                                weight weight = ReadWeight(reader);
                                weightList.Add(weight);
                            }
                            return weightList;
                        }
                    }
                }

            }
            if (id == 3)
            {
                const string selectString = "SELECT top 30 * FROM Weight order by id DESC";
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            List<weight> weightList = new List<weight>();
                            while (reader.Read())
                            {
                                weight weight = ReadWeight(reader);
                                weightList.Add(weight);
                            }
                            return weightList;
                        }
                    }
                }
            }
            if(id == 4)
            {
                const string selectString = "SELECT top 365 * FROM Weight order by id DESC";
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            List<weight> weightList = new List<weight>();
                            while (reader.Read())
                            {
                                weight weight = ReadWeight(reader);
                                weightList.Add(weight);
                            }
                            return weightList;
                        }
                    }
                }
            }

            if (id == 5)
            {
                const string selectString = "SELECT top 7 * FROM  ( SELECT TOP 14 * FROM Weight ORDER BY ID desc) z ORDER BY ID ";
                using (SqlConnection databaseConnection = new SqlConnection(connectionString))
                {
                    databaseConnection.Open();
                    using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            List<weight> weightList = new List<weight>();
                            while (reader.Read())
                            {
                                weight weight = ReadWeight(reader);
                                weightList.Add(weight);
                            }
                            return weightList;
                        }
                    }
                }
            }
            else
            {
                List<weight> weightList = new List<weight>();
                return weightList;
            }
        }

        // POST: api/weight
        [HttpPost]
        public int Post([FromBody] weight w)
        {
            const string postString = "INSERT INTO weight(dato, weight) VALUES (@dato, @weight)";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(postString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@dato", w.dato);
                    insertCommand.Parameters.AddWithValue("@weight", w.weightMeasure);
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected;
                }
            }

        }

        // PUT: api/weight/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
