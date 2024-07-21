using System;


namespace PhoneBookWithFile.Services
{
    internal class LoggingService : ILoggingService
    {
        public void LogInfo(string message)
        {
            Console.Write(message);
        }

        public void LogInfoLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
