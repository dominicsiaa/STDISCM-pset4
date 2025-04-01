using Authentication.Model;

namespace Authentication.Infrastructure
{
    public class DataAccess : IDisposable
    {
        private readonly DataContext _context;

        public DataAccess(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void Dispose() => _context.Dispose();

        public bool RegisterUser(string username, string password, string role)
        {
            if (_context.Users.Any(u => u.Username == username))
                return false;

            _context.Users.Add(new User { Username = username, PasswordHash = password, Role = role });
            _context.SaveChanges();

            var users = _context.Users.ToList();
            Console.WriteLine($"Users count: {users.Count}");

            return true;
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
