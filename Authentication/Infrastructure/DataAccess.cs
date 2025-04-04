using System.Diagnostics.Eventing.Reader;
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

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList(); // Assuming you're using EF
        }


        public bool InsertRefreshToken(RefreshToken refreshToken, int userId)
        {
            if (_context.RefreshTokens.Any(rt => rt.Token == refreshToken.Token))
                return false;
            refreshToken.UserId = userId;
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            return true;
        }

        public bool DisableUserTokensByUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false;

            var refreshTokens = _context.RefreshTokens.Where(rt => rt.UserId == user.Id).ToList();
            if (!refreshTokens.Any())
                return false;

            foreach (var token in refreshTokens)
            {
                token.Enabled = false;
            }

            _context.SaveChanges();
            return true;
        }


        public bool DisableUserToken(string token)
        {
            var refreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
            if (refreshToken == null)
                return false;

            refreshToken.Enabled = false;
            _context.SaveChanges();
            return true;
        }

        public bool IsRefreshTokenValid(string token)
        {
            var refreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
            if (refreshToken == null)
                return false;

            return refreshToken.Enabled && refreshToken.Expires > DateTime.UtcNow;
        }

        public User? FindUserByToken(string token)
        {
            var refreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
            if (refreshToken == null)
                return null;

            return _context.Users.FirstOrDefault(u => u.Id == refreshToken.UserId);
        }
    }
}
