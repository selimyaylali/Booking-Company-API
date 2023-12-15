using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using midterm2.Model;

namespace Repository
{
    public class ClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Client? CreateClient(SignUp modal)
        {
            using var context = new SelimContext(); 
            try
            {
                var client = new Client
                {
                    FirstName = modal.FirstName,
                    LastName = modal.LastName,
                    Username = modal.Username,
                    ClientPassword = modal.Password 
                };
                client = CreateToken(client);

                context.Clients.Add(client);
                context.SaveChanges();
                return client;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Client? GetClientByUsername(string username)
        {
            using var context = new SelimContext(); 
            try
            {
                var client = context.Clients.SingleOrDefault(u => u.Username == username);
                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Client? GetClientByToken(string token)
        {
            using var context = new SelimContext(); 
            try
            {
                var client = context.Clients.SingleOrDefault(u => u.Token == token);
                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Client? GetClientLogin(Login login)
        {
            using var context = new SelimContext(); 
            try
            {
                var client = context.Clients.SingleOrDefault(u => u.Username == login.Username && u.ClientPassword == login.Password);
                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Client CreateToken(Client client)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, client.Username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);
            client.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return client;
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(12),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}