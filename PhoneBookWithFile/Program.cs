using PhoneBookWithFile.Model;
using PhoneBookWithFile.Services;
using System;
using System.Net;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileServiceV2 fileServiceV2 = new FileServiceV2();
            Contact contact = new Contact();
            AllContacts allContacts = new AllContacts();

            do
            {
                Console.WriteLine(" 1 - Add contact");
                Console.WriteLine(" 2 - Search contact");
                Console.WriteLine(" 3 - Remove contact");
                Console.WriteLine(" 4 - Read all contact");
                Console.WriteLine(" 5 - Edit contact");
                Console.WriteLine(" 6 - Delete all contact");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("ism kiriting: ");
                            contact.Name = Console.ReadLine();
                            Console.WriteLine("raqam kiriting: ");
                            contact.PhoneNumber = Console.ReadLine();

                            fileServiceV2.AddContact(contact);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("ism kiriting: ");
                            string name = Console.ReadLine();

                            fileServiceV2.SearchContact(name);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("ism kiriting: ");
                            string name = Console.ReadLine();

                            fileServiceV2.RemoveContact(name);
                            break;
                        }
                    case 4:
                        {
                            fileServiceV2.ShowAllContacts();
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("ism kiriting: ");
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
                        {
                            Console.WriteLine("Enter the right choice on the menu");
                            break;
                        }
                }
            } while (true);
        }
    }
}
