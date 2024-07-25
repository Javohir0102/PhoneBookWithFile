using PhoneBookWithFile.Model;

namespace PhoneBookWithFile.Services
{
    internal interface IFileServiceV2
    {
        void AddContact(Contact contact);
        void SearchContact(string name);
        void RemoveContact(string name);
        void ShowAllContacts();
        void UpdateContact(string name);
        void DeleteAllContact();
    }
}
