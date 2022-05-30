using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatApp.Business.Helpers
{
    public enum Roles
    {
        Admin,
        Member
    }
    public static class Helper
    {
        public static string GeneratorString(string fullname)
        {
            return UtfToAscii(fullname) + RandomNumber(10, 100000).ToString();
        }
        private static int RandomNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }
        private static string UtfToAscii(string fullname = "", int maxLenght = 30)
        {
            int i = fullname.IndexOfAny(new char[] { 'ş', 'ç', 'ö', 'ğ', 'ü', 'ı' });
            string @string = fullname.ToLower();
            if (i > -1)
            {
                StringBuilder outPut = new StringBuilder(@string);
                outPut.Replace('ö', 'o');
                outPut.Replace('ç', 'c');
                outPut.Replace('ş', 's');
                outPut.Replace('ı', 'i');
                outPut.Replace('ğ', 'g');
                outPut.Replace('ü', 'u');
                @string = outPut.ToString();
            }
            @string = Regex.Replace(@string, @"[^a-z0-9\s-]", String.Empty);
            @string = Regex.Replace(@string, @"[\s-]+", " ").Trim();
            @string = @string[..(@string.Length <= maxLenght ? @string.Length : maxLenght)].Trim();
            @string = Regex.Replace(@string, @"\s", "_");
            return @string;
        }
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
