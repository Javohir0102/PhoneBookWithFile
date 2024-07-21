using System;

namespace PhoneBookWithFile.Services
{
    internal interface ILoggingService
    {
        void LogInfoLine(string message);
        void LogInfo(string message);       
    }
}