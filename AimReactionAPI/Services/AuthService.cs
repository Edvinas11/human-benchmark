using AimReactionAPI.Data;
using System.Security.Cryptography;
using System.Text;

namespace AimReactionAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GameService> _logger;

        public AuthService(AppDbContext context, ILogger<GameService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == storedHash;
        }
    }
}
