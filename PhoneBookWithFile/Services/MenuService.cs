using PhoneBookWithFile.Model;
using Spectre.Console;
using System;

namespace PhoneBookWithFile.Services
{
    internal class MenuService
    {
        IFileServiceV2 fileServiceV2;
        AllContacts AllContacts = new AllContacts();
        Contact contact = new Contact();
        ILoggingService log = new LoggingService();



        public void ShowMenuService()
        {
            AnsiConsole.MarkupLine(" [red]Welcome to the[/] [orange1]Contact app[/]");

            var fileType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(" Choose [green]file type:[/]")
                    .PageSize(5)
                    .AddChoices(new[] {
            "Json file", "Txt file",
                    }));

            switch (fileType)
            {
                case "Json file":
                    {
                        fileServiceV2 = new FileServiceV2();
                        break;
                    }
                case "Txt file":
                    {
                        fileServiceV2 = new FileService();
                        break;
                    }
                default:
                    {
                        AnsiConsole.MarkupLine(" [green]Invalid choice![/]");
                        break;
                    }
            }

            string selction;
            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine($" You are in [bold blue]{fileType}[/]!");

                try
                {
                    // Ask for the user's favorite fruit
                    var menuChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title(" Select your [green]choice[/]?")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more services)[/]")
                            .AddChoices(new[] {
            "Read all contacts", "Search contact", "Add contact",
            "Remove contact", "Edit contact", "Clear all contacts"
                            }));

                    // Echo the fruit back to the terminal
                    AnsiConsole.WriteLine($" Your choice: {menuChoice}");
                    switch (menuChoice)
                    {
                        case "Read all contacts":
                            fileServiceV2.ShowAllContacts();
                            break;
                        case "Search contact":
                            {
                                AnsiConsole.Markup(" [salmon1]ism kiriting:[/] ");
                                string name = Console.ReadLine();

                                fileServiceV2.SearchContact(name);
                                break;
                            }
                        case "Add contact":
                            {
                                AnsiConsole.Markup(" [salmon1]ism kiriting:[/] ");
                                contact.Name = Console.ReadLine();
                                AnsiConsole.Markup(" [salmon1]raqam kiriting:[/] ");
                                contact.PhoneNumber = Console.ReadLine();

                                fileServiceV2.AddContact(contact);
                                break;
                            }
                        case "Remove contact":
                            {
                                AnsiConsole.Markup(" [maroon]ism kiriting:[/] ");
                                string name = Console.ReadLine();

                                fileServiceV2.RemoveContact(name);
                                break;
                            }
                        case "Edit contact":
                            {
                                AnsiConsole.Markup(" [maroon]ism kiriting:[/] ");
                                string name = Console.ReadLine();

                                fileServiceV2.UpdateContact(name);
                                break;
                            }
                        case "Clear all contacts":
                            {
                                fileServiceV2.DeleteAllContact();
                                break;
                            }
                        default:
                            AnsiConsole.MarkupLine(" [red]Enter the correct choice! on the menu\n Your Choice:[/] ");
                            break;
                    }
                }                
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    AnsiConsole.MarkupLine
                        (" [bold yellow]If you make the right choice, " +
                        "and the program is working incorrectly, " +
                        "please contact us[/]");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                AnsiConsole.MarkupLine(" [red]Do you want using again?[/]");

                selction = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(5)
                        .AddChoices(new[] { "Yes", "No", }));
            } while (selction is "Yes");
        }
    }
}
