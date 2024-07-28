using Newtonsoft.Json;
using PhoneBookWithFile.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;

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
            Console.Clear();
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
            Console.Clear();
            string jsonFile = File.ReadAllText(filePath);
            var result = JsonConvert.DeserializeObject<AllContacts>(jsonFile);
            return result;
        }

        public void RemoveContact(string name)
        {
            Console.Clear();

            AllContacts allContactList = GetContactsFromJson();
            if (allContactList is null)
            {
                AnsiConsole.MarkupLine(" [red]Contact is not found[/]");
            }
            else
            {
                foreach (var item in allContactList.Contacts)
                {
                    if (name.ToUpper() == item.Name.ToUpper())
                    {
                        allContactList.Contacts.Remove(item);
                        AnsiConsole.MarkupLine($" [bold thistle1]{item.Name}[/] is successfully deleted.");
                        break;
                    }
                }
                string allContactString = JsonConvert.SerializeObject(allContactList, Formatting.Indented);
                DeleteAndCreateFile(allContactString);
            }
        }

        public void SearchContact(string name)
        {
            Console.Clear();
            SelectedContact = null;
            string[] allContacts = File.ReadAllLines(filePath);
            var contactList = GetContactsFromJson();

            foreach (var item in contactList.Contacts)
            {
                if (name.ToUpper() == item.Name.ToUpper())
                {
                    SelectedContact = item;
                    AnsiConsole.MarkupLine($" Name: [bold cornflowerblue]{item.Name}[/], Phone number: [bold cornflowerblue]{item.PhoneNumber}[/]");
                    break;
                }
            }
            if (SelectedContact is null)
            {
                AnsiConsole.MarkupLine($" [bold teal]{name}[/] [red]is not found[/]");
            }
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            var contactList = GetContactsFromJson();

            if (contactList is null)
            {
                AnsiConsole.MarkupLine(" [red]Contact is not found.[/]");
                return;
            }
            foreach (Contact item in contactList.Contacts)
            {
                AnsiConsole.MarkupLine($" Name: [bold aqua]{item.Name}[/], Phone number: [bold aqua]{item.PhoneNumber}[/]");
            }
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
            Console.Clear();
            SearchContact(name);
            AnsiConsole.Markup(" [bold yellow]1. change name\n 2. change number\n selection: [/]");
            int selection = int.Parse(Console.ReadLine());

            var allContactList = GetContactsFromJson();

            if (selection == 1)
            {
                AnsiConsole.Markup(" [aqua]Enter the new name[/]: ");
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
                AnsiConsole.Markup(" [aqua]Enter the new number[/]: ");
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
            AnsiConsole.MarkupLine(" [bold red]All contact is deleted![/]");
        }
    }
}
