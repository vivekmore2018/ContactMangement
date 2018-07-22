using CM.Models;
using System.Collections.Generic;

namespace CM.Interface.Business
{
    public interface IContactManager
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        BusinessResult<Contact> Update(Contact contact);
        int ChangeStatus(int id, bool status);
        BusinessResult<Contact> AddContact(Contact contact);
        int DeleteContact(int id);
    }
}
