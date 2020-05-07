using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AzureFunctionsBofin
{
    public static class Function1
    {
        [FunctionName("ProductPrice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Get the connection string from app settings and use it to create a connection.
            var str = Environment.GetEnvironmentVariable("sql_connection");
            

            string id = req.Query["id"];
            int rto = -1;
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var query = "SELECT * FROM Product WHERE Id = " + id;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        rto = int.Parse(reader["Price"].ToString());
                        //["Id"], ["Name"],  ["Price"]

                    }
                }
            }

            return rto != -1
                ? (ActionResult)new OkObjectResult(rto)
                : new BadRequestObjectResult("Something is wrong");
        }
    }
}
