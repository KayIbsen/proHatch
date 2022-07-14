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
using Microsoft.Extensions.Configuration;

namespace SensorService
{
    public static class SensorService
    {
        [FunctionName("SensorService")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");


            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SensorRecord_DTO sensorRecord = JsonConvert.DeserializeObject<SensorRecord_DTO>(requestBody);

            // Make a validation of incomming data, before configure and sending to db

            sensorRecord.Id = Guid.NewGuid().ToString();
            sensorRecord.UnitId = "4cdc6b0e-9d26-4a2a-b98f-5885b756063a";
            sensorRecord.TimeStamp = DateTime.Now.ToString("yyyy-MM-dd h:mm tt");


            // 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var text = $"INSERT INTO [dbo].[SensorRecord] ([Id],[UnitId],[TimeStamp],[Temperature],[Humidity],[PPM]) " +
                    $"VALUES ('{sensorRecord.Id}','{sensorRecord.UnitId}','{sensorRecord.TimeStamp}',{sensorRecord.Temperature},{sensorRecord.Humidity},{sensorRecord.PPM})";

                
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    try
                    {
                        var rows = await cmd.ExecuteNonQueryAsync();
                        log.LogInformation($"{rows} rows were updated");
                    }
                    catch (Exception e)
                    {

                        log.LogError(e.Message);
                    }

                    


                }
                


            }


            return new OkResult();
        }
    }
}
