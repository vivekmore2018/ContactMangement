using CM.Models;
using System.Collections.Generic;

namespace CM.Interface.DataAccess
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        int Update(Contact contact);
        int ChangeStatus(int id, bool status);
        int AddContact(Contact contact);
    }
}
