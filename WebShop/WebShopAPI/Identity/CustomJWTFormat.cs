using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;


namespace WebShopAPI.Identity
{
    public class CustomJWTFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private static readonly byte[] _secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
        private readonly string _issuer;

        public CustomJWTFormat() : this(string.Empty) { }

        public CustomJWTFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var signingKey = new SigningCredentials(new SymmetricSecurityKey(_secret), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(_issuer, "Any", data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey));
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }

        public JwtSecurityToken GetDataFromEncodedTocken(string encodedToken)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(encodedToken) as JwtSecurityToken;
        }
    }
}