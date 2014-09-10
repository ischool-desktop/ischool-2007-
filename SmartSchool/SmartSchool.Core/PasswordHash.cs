﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace SmartSchool
{
    public static class PasswordHash
    {
        public static string Compute(string password)
        {
            SHA1Managed sha1 = new SHA1Managed();
            Encoding utf8 = Encoding.UTF8;

            byte[] hashResult = sha1.ComputeHash(utf8.GetBytes(password));

            return Convert.ToBase64String(hashResult);
        }
    }
}
