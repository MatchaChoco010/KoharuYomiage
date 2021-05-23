using System;
using System.Text.RegularExpressions;

namespace KoharuYomiageApp.Domain.Account
{
    public record Username
    {
        public Username(string username)
        {
            if (!Regex.IsMatch(username, @"^[^@]+$"))
            {
                throw new ArgumentException("Invalid Username");
            }

            Value = username;
        }

        public string Value { get; }
    }
}
