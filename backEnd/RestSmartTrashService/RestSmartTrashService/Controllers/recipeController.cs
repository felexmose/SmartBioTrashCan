using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSmartTrashService.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestSmartTrashService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class recipeController : ControllerBase
    {
        private static string recipeUri = "https://restsmarttrashservice.azurewebsites.net/api/recipe";

        public static async Task<IList<recipe1>> GetRecipeAsync(int id)
        {
            string requestUri = recipeUri + "/" + id;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(requestUri);
                IList<recipe1> c = JsonConvert.DeserializeObject<IList<recipe1>>(content);

                return c;
            }
        }
        string connectionString = "Server=tcp:projektgruppe.database.windows.net,1433;Initial Catalog=smartbiotrashcanDB;Persist Security Info=False;User ID=projektGruppe;Password=1234ABcd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
       
       
        private static recipe1 ReadRecipe(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string dato = reader.GetString(1);
            string recipe = reader.GetString(2);
           
        
            recipe1 recipe3 = new recipe1
            {
                id = id,
                dato = dato,
                recipe = recipe,
            };
            return recipe3;

        }

        // GET: api/recipe/5
        [HttpGet("{id}", Name = "Get2")]
        public IEnumerable<recipe1> Get(int id)
        {
            const string selectString = "SELECT * FROM Recipe order by id DESC";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<recipe1> recipeList = new List<recipe1>();
                        while (reader.Read())
                        {
                            recipe1 weight = ReadRecipe(reader);
                            recipeList.Add(weight);
                        }
                        return recipeList;
                    }
                }
            }
        }

        // POST: api/recipe
        [HttpPost]
        public int Post([FromBody] recipe1 r)
        {
            const string postString = "INSERT INTO recipe(dato, recipe ) VALUES (@dato, @recipe";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(postString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@dato", r.dato);
                    insertCommand.Parameters.AddWithValue("@recipe", r.recipe);
                   
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected;
                }
            }
        }

        // PUT: api/recipe/5
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
