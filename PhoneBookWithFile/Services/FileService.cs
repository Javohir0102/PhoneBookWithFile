using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PhoneBookWithFile.Services
{
    internal class FileService : IFileService
    {
        private const string filePath = "../../../phoneBook.txt";
        private ILoggingService loggingService;
        public FileService() 
        {
            this.loggingService = new LoggingService();
            EnsureFileExists();
        }
        public void AddContact()
        {
            Console.Write("Enter the name: ");
            string name = Console.ReadLine();
            Console.Write("Enter the phone number: ");
            string phoneNumber = Console.ReadLine();
            string contact = $"{name}, {phoneNumber}";
            File.AppendAllText(filePath, contact + Environment.NewLine);            
        }

        public void RemoveContact()
        {
            Console.Write("Enter the name: ");
            string name = Console.ReadLine();
            string[] allContacts = File.ReadAllLines(filePath);
            string tempAllContacts = string.Empty;
            foreach (var item in allContacts)
            {                
                string splitName = item.Split(",")[0];
                if (!splitName.ToUpper().Equals(name.ToUpper()))
                {
                    tempAllContacts += item + "\n";
                }                
            }
            File.Delete(filePath);
            EnsureFileExists();
            File.AppendAllText(filePath, tempAllContacts);
        }

        public void ReadAllContacts()
        {
            string txt = File.ReadAllText(filePath);
            Console.WriteLine(txt);
        }

        public void SearchContact()
        {
            Console.WriteLine("Search name: ");
            string searchName = Console.ReadLine();
            string[] allContacts = File.ReadAllLines(filePath);

            foreach (var contact in allContacts)
            {
                string splitName = contact.Split(",")[0];
                if(searchName.ToUpper().Equals(searchName.ToUpper()))
                {
                    Console.WriteLine(contact);
                }
                else
                {
                    Console.WriteLine("Contact is not found! try again.");
                }
            }
        }

        private static void EnsureFileExists()
        {
            bool isFilePresent = File.Exists(filePath);
            if (!isFilePresent)
            {
                File.Create(filePath).Close();
            }
        }

    }
}
