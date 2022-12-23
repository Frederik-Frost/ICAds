using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace ICAds.Security
{
    public class SecurityManager
    {
        public static IConfiguration Configuration;

        public static void SetConfig(IConfiguration config)
        {
            Configuration = config;
        }

        public static EncryptedPassword HashPassword(string password)
      
        {
            using (var hmac = new HMACSHA512())
            {

                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return new EncryptedPassword(hash, salt);
            }
        }
        
        public static bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {


            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var newHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


                return newHash.SequenceEqual(passwordHash);
            }


        }

        public static string CreateToken(string userId, string orgId, string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("OrganizationId", orgId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



    }
    public class EncryptedPassword
    {

        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }

        public EncryptedPassword(byte[] hash, byte[] salt)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}

