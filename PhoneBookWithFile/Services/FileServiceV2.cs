using Newtonsoft.Json;
using PhoneBookWithFile.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;

namespace PhoneBookWithFile.Services
{
    internal class FileServiceV2 : IFileServiceV2
    {
        private const string filePath = "../../../phoneBook.json";
        private ILoggingService log;
        private Contact SelectedContact;
        public FileServiceV2()
        {
            this.log = new LoggingService();
            EnsureFileExist();
        }

        public void AddContact(Contact contact)
        {
            List<Contact> user = null;
            AllContacts allContactList = GetContactsFromJson();
            if (allContactList is null)
            {
                allContactList = new AllContacts();
                user = new List<Contact>();
                user.Add(contact);
                allContactList.Contacts = user;
            }
            else
            {
                allContactList.Contacts.Add(contact);
            }

            string allContactString = JsonConvert.SerializeObject(allContactList, Formatting.Indented);
            DeleteAndCreateFile(allContactString);
        }

        private static AllContacts GetContactsFromJson()
        {
            string jsonFile = File.ReadAllText(filePath);
            var result = JsonConvert.DeserializeObject<AllContacts>(jsonFile);
            return result;
        }

        public void RemoveContact(string name)
        {
            List<Contact> user = null;
            AllContacts allContactList = GetContactsFromJson();
            if (allContactList is null)
            {
                Console.WriteLine("Contact is not found");
            }
            else
            {
                foreach (var item in allContactList.Contacts)
                {
                    if (name.ToUpper() == item.Name.ToUpper())
                    {
                        allContactList.Contacts.Remove(item);
                        Console.WriteLine($" {item.Name} is successfully deleted.");
                        break;
                    }
                }
                string allContactString = JsonConvert.SerializeObject(allContactList, Formatting.Indented);
                DeleteAndCreateFile(allContactString);
            }
        }

        public void SearchContact(string name)
        {
            SelectedContact = null;
            string[] allContacts = File.ReadAllLines(filePath);
            var contactList = GetContactsFromJson();

            foreach (var item in contactList.Contacts)
            {
                if (name.ToUpper() == item.Name.ToUpper())
                {
                    SelectedContact = item;
                    Console.WriteLine($"Name: {item.Name}, Phone number: {item.PhoneNumber}");
                    break;
                }
            }
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            var contactList = GetContactsFromJson();

            if (contactList is null)
            {
                Console.WriteLine("Contact is not found.");
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            foreach (Contact item in contactList.Contacts)
            {
                Console.WriteLine($"Name: {item.Name}, Phone number: {item.PhoneNumber}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void EnsureFileExist()
        {
            bool isFilePresent = File.Exists(filePath);
            if (!isFilePresent)
            {
                File.Create(filePath).Close();
            }
        }

        public void DeleteAndCreateFile(string contacts)
        {
            File.Delete(filePath);
            EnsureFileExist();
            File.WriteAllText(filePath, contacts);
        }

        public void UpdateContact(string name)
        {
            SearchContact(name); 
            Console.WriteLine("1. change name\n2. change number");
            int selection = int.Parse(Console.ReadLine());

            var allContactList = GetContactsFromJson();

            if (selection == 1)
            {
                string newName = Console.ReadLine();
                foreach (var item in allContactList.Contacts)
                {
                    if (name.ToUpper() == item.Name.ToUpper())
                    {
                        allContactList.Contacts.Remove(item);
                        break;
                    }
                }
                SelectedContact.Name = newName;
                allContactList.Contacts.Add(SelectedContact);
            }
            else if (selection == 2)
            {
                string newNumber = Console.ReadLine();
                foreach (var item in allContactList.Contacts)
                {
                    if (name.ToUpper() == item.Name.ToUpper())
                    {
                        allContactList.Contacts.Remove(item);
                        break;
                    }
                }
                SelectedContact.PhoneNumber = newNumber;
                allContactList.Contacts.Add(SelectedContact);
            }
            string allContactString = JsonConvert.SerializeObject(allContactList, Formatting.Indented);
            DeleteAndCreateFile(allContactString);
        }

        public void DeleteAllContact()
        {
            File.WriteAllText(filePath, "");
            Console.WriteLine("All contact is deleted!");
        }
    }
}
