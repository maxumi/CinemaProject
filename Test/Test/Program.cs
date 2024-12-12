using System;
using BCrypt.Net;

class Program
{
    static void Main()
    {
        string plainTextPassword = "Password";
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);

        Console.WriteLine($"Plain Text Password: {plainTextPassword}");
        Console.WriteLine($"Hashed Password: {hashedPassword}");
    }
}
