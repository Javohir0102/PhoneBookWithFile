using PhoneBookWithFile.Model;
using System;
using System.IO;

namespace PhoneBookWithFile.Services
{
    internal class FileService : IFileServiceV2
    {
        private const string filePath = "../../../phoneBook.txt";
        private ILoggingService log;
        private string SelectedContact;
        public FileService()
        {
            this.log = new LoggingService();
            EnsureFileExists();
        }
        public void AddContact(Contact contact)
        {
            Console.Clear();
            string newContact = $"{contact.Name}, {contact.PhoneNumber}";
            File.AppendAllText(filePath, newContact + Environment.NewLine);
            log.LogInfoLine(" Contact is successfully added\n");
        }

        public void RemoveContact(string name)
        {
            Console.Clear();

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
            DeleteAndCreateFile(tempAllContacts);
            log.LogInfoLine(" Contact is successfully deleted\n");
        }

        private void DeleteAndCreateFile(string tempAllContacts)
        {
            File.Delete(filePath);
            EnsureFileExists();
            File.AppendAllText(filePath, tempAllContacts);
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            string txt = File.ReadAllText(filePath);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            log.LogInfoLine(txt);
            Console.ForegroundColor= ConsoleColor.White;
        }

        public void SearchContact(string name)
        {
            Console.Clear();
            SelectedContact = string.Empty;
            string[] allContacts = File.ReadAllLines(filePath);

            foreach (var contact in allContacts)
            {
                string splitName = contact.Split(",")[0];
                if (splitName.ToUpper().Equals(name.ToUpper()))
                {
                    log.LogInfoLine(contact);
                    SelectedContact = contact;
                }
            }
            if (SelectedContact == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                log.LogInfoLine(" Contact is not found! try again.");
                Console.ForegroundColor = ConsoleColor.White;
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

        public void UpdateContact(string name)
        {
            Console.Clear();
            SearchContact(name);
            if (!SelectedContact.Equals(""))
            {
                log.LogInfoLine(" 1. change name\n 2. change number");
                int selection = int.Parse(Console.ReadLine());
                string contacts = File.ReadAllText(filePath);
                string[] nameAndNumber = SelectedContact.Split(",");
                string updatedContact = string.Empty;

                if (selection == 1)
                {
                    log.LogInfo(" Enter the new name: ");
                    string newName = Console.ReadLine();
                    updatedContact = contacts.Replace(nameAndNumber[0], newName);
                    log.LogInfoLine(" Name is successfully edited\n");
                }
                else if (selection == 2)
                {
                    log.LogInfo(" Enter the new phone number: ");
                    string newPhoneNumber = Console.ReadLine();
                    updatedContact = contacts.Replace(nameAndNumber[1], newPhoneNumber);
                    log.LogInfoLine(" Phone number is successfully edited\n");
                }
                DeleteAndCreateFile(updatedContact);

            }
        }

        public void DeleteAllContact()
        {
            Console.Clear();
            File.WriteAllText(filePath, "");
            Console.ForegroundColor = ConsoleColor.Red;
            log.LogInfoLine(" All contacts are successfully cleared\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}