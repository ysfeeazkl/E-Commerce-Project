﻿namespace E_Commerce.Shared.Utilities.Generators
{
    public static class VerificationCodeGenerator
    {
        public static string Generate()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
