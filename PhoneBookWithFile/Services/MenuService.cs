using PhoneBookWithFile.Model;
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
            log.LogInfoLine(" Welcome to the Contact app");
            log.LogInfo(" Choose file type: 1 - Json | 2 - Txt\n Your choice: ");
            string fileType = Console.ReadLine();

            if (fileType == "1")
            {
                fileServiceV2 = new FileServiceV2();
            }
            else if (fileType == "2") 
            {
                fileServiceV2 = new FileService();
            }

            string selction;
            do
            {
                Console.Clear();
                log.LogInfoLine(" 1. Read all contact");
                log.LogInfoLine(" 2. Search contact");
                log.LogInfoLine(" 3. Add contact");
                log.LogInfoLine(" 4. Remove contact");
                log.LogInfoLine(" 5. Edit contact");
                log.LogInfoLine(" 6. Clear all contacts\n");

                log.LogInfo(" Enter your choice: ");
                try
                {
                    int yourChoice = int.Parse(Console.ReadLine());
                    switch (yourChoice)
                    {
                        case 1:
                            fileServiceV2.ShowAllContacts();
                            break;
                        case 2:
                            {
                                log.LogInfo(" ism kiriting: ");
                                string name = Console.ReadLine();

                                fileServiceV2.SearchContact(name);
                                break;
                            }
                        case 3:
                            {
                                log.LogInfo(" ism kiriting: ");
                                contact.Name = Console.ReadLine();
                                log.LogInfo(" raqam kiriting: ");
                                contact.PhoneNumber = Console.ReadLine();

                                fileServiceV2.AddContact(contact);
                                break;
                            }
                        case 4:
                            {
                                log.LogInfo(" ism kiriting: ");
                                string name = Console.ReadLine();

                                fileServiceV2.RemoveContact(name);
                                break;
                            }
                        case 5:
                            {
                                log.LogInfo(" ism kiriting: ");
                                string name = Console.ReadLine();

                                fileServiceV2.UpdateContact(name);
                                break;
                            }
                        case 6:
                            {
                                fileServiceV2.DeleteAllContact();
                                break;
                            }
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            log.LogInfoLine(" Enter the correct choice! on the menu\n Your Choice: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine(" Wrong!!! please enter only number(between 1 and 5)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine(" Wrong!!! please enter 1, 2, 3, 4 or 5");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine
                        (" If you make the right choice, " +
                        "and if the program is working incorrectly, " +
                        "please contact us");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                log.LogInfo(" Do you want using again?\n please press (yes) or (y): ");
                selction = Console.ReadLine().ToLower();
            } while (selction is "yes" or "y");
        }
    }
}
