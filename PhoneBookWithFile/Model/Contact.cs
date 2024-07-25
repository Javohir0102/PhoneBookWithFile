using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWithFile.Model
{
    internal class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    internal class AllContacts
    {
        public List<Contact> Contacts { get; set;}
    }
}
