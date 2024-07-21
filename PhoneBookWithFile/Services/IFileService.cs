namespace PhoneBookWithFile.Services
{
    internal interface IFileService
    {
        void AddContact();
        void SearchContact();
        void RemoveContact();
        void ReadAllContacts();
        void UpdateContact();
        void ClearAllContacts();
    }
}