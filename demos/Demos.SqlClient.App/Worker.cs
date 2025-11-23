using Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Demos.SqlClient.App
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<DatabaseOptions> _dbOptions;

        public Worker(ILogger<Worker> logger, IOptions<DatabaseOptions> dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string queryString = "SELECT * FROM [dbo].[User];";

            using (SqlConnection connection = new SqlConnection(_dbOptions.Value.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    var record = (IDataRecord)reader;
                    var name = record["Name"];
                    var email = record["Email"];

                    _logger.LogInformation($"{name}, {email}");
                }

                // Call Close when done reading.
                reader.Close();
            }

            _logger.LogInformation("Query completed.");
            return Task.CompletedTask;
        }
    }
}