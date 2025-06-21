using StudyToolFlashcardUpscaler.Models.Dtos;

namespace StudyToolFlashcardUpscaler.Api.Services
{
    public class UserService
    {
        private readonly DatabaseService _database;
        readonly Random rnd = new Random();

        public UserService(DatabaseService database)
        {
            _database = database;
            _database.LoadData();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _database.GetUsers();
        }

        public UserDto? GetUser(string username, string password)
        {
            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            // {
            //     return null; //return null if user credentials are not provided
            // }

            if (_database.Data?.users == null)
            {
                return null; //return null if no users exist
            }
           // return _database.GetUsers().FirstOrDefault(user => user.Id == id);
       
        var user = _database.GetUsers().FirstOrDefault(user => user.username == username && user.password == password);
            if (user == null)
            {
                return null; //return null if no user matches the credentials
            }

            return user; //return the found user
        }

        public UserDto CreateUser(UserDto newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "User data cannot be null.");
            }

            if (_database.Data!.users == null)
                _database.Data.users = [];

            var highestId = _database.Data.users.Max(x => x.Id);
            newUser.Id = highestId + 1;

            _database.Data.users!.Add(newUser);
            _database.SaveData();

            return newUser;
        }
    }
}