﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class Authentification
{
    internal static ClaimsPrincipal DecodeToken(string token, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = JsonConvert.DeserializeObject<byte[]>(secretKey) ?? new byte[0];

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        };

        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        return principal;
    }

    internal static int GetIdOfCurrentUser(string token, string secretKey)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = JsonConvert.DeserializeObject<byte[]>(secretKey) ?? new byte[0];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            return int.Parse(principal.Claims.ElementAt(0).Value);
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    internal static string GetRoleOfCurrentUser(string token, string secretKey)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = JsonConvert.DeserializeObject<byte[]>(secretKey) ?? new byte[0];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            return principal.Claims.ElementAt(1).Value;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    internal static bool CheckAuthorization(string tokenStr, string keyStr, string role)
    {
        try
        {
            if (tokenStr == null || tokenStr == "" || keyStr == null || keyStr == "")
            {
                return false;
            }

            ClaimsPrincipal claimsPrincipal = DecodeToken(tokenStr, keyStr);
            return claimsPrincipal.IsInRole(role);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    internal static List<string> GenerateToken(int userId, string role)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(GenerateRandomSecretKey(32));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new List<string>() { tokenHandler.WriteToken(token), JsonConvert.SerializeObject(key) };
    }

    private static string GenerateRandomSecretKey(int length)
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder stringBuilder = new StringBuilder();
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (length-- > 0)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                stringBuilder.Append(validChars[(int)(num % (uint)validChars.Length)]);
            }
        }
        return stringBuilder.ToString();
    }
}