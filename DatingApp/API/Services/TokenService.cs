using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;// this Type uses just one key to both sign and encrypt the token. The other Type is `Asymmetric`

        //contructor
        public TokenService(IConfiguration config)
        {
            // congif is a dictionary of congif porps for hte token.
            // this takes the 'TokenKey'(that we create later....) and converts it to a byte[]
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); // TokenKey is a key that we will create later.
        }

        public string CreateToken(AppUser user)
        {
            //this is a list of Claim Type 
            var claims = new List<Claim>
            {
                // a claim is added to the List<> with 2 parameters.
                // The Claim Type ("nameId"), and the value, which here is the users name (UserName).
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            // this creates credentials that will be placed in the TokenDescriptor.
            // the SigningCredentials Class takes the _key (byte[] above) and the algorithm used encrypt it. 
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            // this is the token itself.
            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),   // the actual data.
                Expires = DateTime.Now.AddDays(20),     // when the token expires.
                SigningCredentials = creds              // the key and algoruthm created above.
            };

            // TokenHandler is the wrapper of the token.
            var tokenHandler = new JwtSecurityTokenHandler();   // create an instsance of the handler
            var token = tokenHandler.CreateToken(tokenDesriptor);//input the token
            return tokenHandler.WriteToken(token);              // serialize the token and return it.
        }
    }
}