using PhoneBookWithFile.Model;
using Spectre.Console;
using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneBookWithFile.Services
{
    internal class FileService : IFileServiceV2
    {
        private const string filePath = "../../../phoneBook.txt";
        private ILoggingService log;
        private string SelectedContact;
        private Table table;
        public FileService()
        {
            table = new Table();
            table.AddColumn("#");
            table.AddColumn("Name");
            table.AddColumn("Phone number");
            this.log = new LoggingService();
            EnsureFileExists();
        }
        public void AddContact(Contact contact)
        {
            Console.Clear();
            string newContact = $"{contact.Name}, {contact.PhoneNumber}";
            File.AppendAllText(filePath, newContact + Environment.NewLine);
            AnsiConsole.MarkupLine(" [red]Contact is successfully added\n[/]");
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
            AnsiConsole.MarkupLine(" [red]Contact is successfully deleted\n[/]");
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
            int id = 1;           
            string txt = File.ReadAllText(filePath);
            string[] entries = txt.Split(Environment.NewLine);
            foreach (var item in entries)
            {
                string[] strings = item.Split(",");
                if (strings.Length == 2)
                {
                    table.AddRow((id++).ToString(), strings[0], strings[1]);
                }
            }
            AnsiConsole.Write(table);
           // AnsiConsole.MarkupLine($" [bold yellow]{txt}[/]");
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
                    table.AddRow("1", contact.Split(",")[0], contact.Split(",")[1]);
                    //AnsiConsole.MarkupLine($" [bold maroon]{contact}[/]");
                    SelectedContact = contact;
                }
                AnsiConsole.Write(table);
            }
            if (SelectedContact == "")
            {
                AnsiConsole.MarkupLine(" [bold red]Contact is not found! try again.[/]");
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
                AnsiConsole.Markup(" [yellow]1. change name\n 2. change number\n your choice: [/]");
                int selection = int.Parse(Console.ReadLine());
                string contacts = File.ReadAllText(filePath);
                string[] nameAndNumber = SelectedContact.Split(",");
                string updatedContact = string.Empty;

                if (selection == 1)
                {
                    AnsiConsole.Markup(" [teal]Enter the new name:[/] ");
                    string newName = Console.ReadLine();
                    updatedContact = contacts.Replace(nameAndNumber[0], newName);
                    AnsiConsole.MarkupLine(" [bold red]Name is successfully edited\n[/]");
                }
                else if (selection == 2)
                {
                    AnsiConsole.Markup(" [teal]Enter the new phone number:[/] ");
                    string newPhoneNumber = Console.ReadLine();
                    updatedContact = contacts.Replace(nameAndNumber[1], newPhoneNumber);
                    AnsiConsole.MarkupLine(" [bold red]Phone number is successfully edited\n[/]");
                }
                DeleteAndCreateFile(updatedContact);
            }
        }

        public void DeleteAllContact()
        {
            Console.Clear();
            File.WriteAllText(filePath, "");
            AnsiConsole.MarkupLine(" [bold olive]All contacts are successfully cleared\n[/]");
        }
    }
}