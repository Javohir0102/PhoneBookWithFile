using PhoneBookWithFile.Services;
using System;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileService fileService = new FileService();
            //fileService.AddContact($"{name}, {phoneNumber}");
            fileService.ReadAllContacts();            
        }
    }
}
