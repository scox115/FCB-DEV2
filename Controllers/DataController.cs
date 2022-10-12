using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace FCB_DEV2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            CreateDB();
            return GetUsers();            
        }

        [HttpPost]
        public int Post(User user)
        {
            CreateDB();
            return AddUser(user);            
        }
    
        public void CreateDB()
        {
            using (var connection = new SqliteConnection("Data Source=fcbdev.db"))
            {  
                connection.Open();                
                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY AUTOINCREMENT, firstName TEXT, lastName TEXT, zipCode TEXT)";
                command.ExecuteNonQuery();          
            }
        }

        public int AddUser(User user)
        {
            using (var connection = new SqliteConnection("Data Source=fcbdev.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO users (firstName, lastName, zipCode) VALUES ($firstName, $lastName, $zipCode)";
                command.Parameters.AddWithValue("$firstName", user.FirstName);
                command.Parameters.AddWithValue("$lastName", user.LastName);
                command.Parameters.AddWithValue("$zipCode", user.ZipCode);
                command.Prepare();
                return command.ExecuteNonQuery();
            }
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();
            using (var connection = new SqliteConnection("Data Source=fcbdev.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT id, firstName, lastName, zipCode FROM users";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new FCB_DEV2.User
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            ZipCode = reader.GetString(3),
                        });                        
                    }
                }
            }
            return users;
        }
    }   
}