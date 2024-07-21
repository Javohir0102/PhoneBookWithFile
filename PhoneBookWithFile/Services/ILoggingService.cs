using System;

namespace PhoneBookWithFile.Services
{
    internal interface ILoggingService
    {
        void LogInformation(string message) => 
            Console.WriteLine(message);
    }
}