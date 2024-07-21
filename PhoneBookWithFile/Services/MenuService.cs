using System;

namespace PhoneBookWithFile.Services
{
    internal class MenuService
    {
        IFileService fileService = new FileService();
        ILoggingService log = new LoggingService();
        public void ShowMenuService() 
        {
            string selction;
            do
            {
                Console.Clear();
                log.LogInfoLine("Welcome to the Contact app");
                log.LogInfoLine("1. Read all contact");
                log.LogInfoLine("2. Search contact");
                log.LogInfoLine("3. Add contact");
                log.LogInfoLine("4. Remove contact");
                log.LogInfoLine("5. Edit contact");
                log.LogInfoLine("6. Clear all contacts\n");

                log.LogInfo("Enter your choice: ");
                try
                {
                    int yourChoice = int.Parse(Console.ReadLine());
                    switch ( yourChoice )
                    {
                        case 1: 
                            fileService.ReadAllContacts();
                            break;
                        case 2:
                            fileService.SearchContact();
                            break;
                        case 3:
                            fileService.AddContact();
                            break;
                        case 4:
                            fileService.RemoveContact();
                            break;
                        case 5:
                            fileService.UpdateContact();
                            break;
                        case 6:
                            fileService.ClearAllContacts();
                            break;
                        default: 
                            Console.ForegroundColor = ConsoleColor.Red;
                            log.LogInfoLine("Enter the correct choice! on the menu");
                            Console.ForegroundColor= ConsoleColor.White;
                            break;
                    }
                }
                catch (FormatException )
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine("Wrong!!! please enter only number(between 1 and 5)");
                    Console.ForegroundColor= ConsoleColor.White;
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine("Wrong!!! please enter 1, 2, 3, 4 or 5");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine
                        ("If you make the right choice, " +
                        "and if the program is working incorrectly, " +
                        "please contact us");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                log.LogInfo("Do you want using again?\nplease press (yes) or (y): ");
                selction = Console.ReadLine().ToLower();
            }while (selction is "yes" or "y");
        }
    }
}
