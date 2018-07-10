using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Interface.Business
{
    public interface IContactManager
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        BusinessResult<Contact> Update(Contact contact);
        int ChangeStatus(int id, bool status);
        BusinessResult<Contact> AddContact(Contact contact);
    }
}
