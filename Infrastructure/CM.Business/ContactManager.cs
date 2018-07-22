using System.Collections.Generic;
using CM.Interface.Business;
using CM.Models;
using CM.Interface.DataAccess;
using System.Text.RegularExpressions;

namespace CM.Business
{
    public class ContactManager : IContactManager
    {
        private IContactRepository _contactRepository;
        private string EmailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public ContactManager(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }
        public Contact GetById(int id)
        {
            return _contactRepository.GetById(id);
        }
        public BusinessResult<Contact> Update(Contact contact)
        {
            BusinessResult<Contact> result = new BusinessResult<Contact>();
            bool isEmail = Regex.IsMatch(contact.Email, EmailRegex, RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                result.Errors.Add("Invalid email address");
            }
            if (result.Errors.Count == 0 && _contactRepository.Update(contact) > 0)
            {
                result.Value = contact;
            }
            return result;
        }
        public int ChangeStatus(int id, bool status)
        {
            return _contactRepository.ChangeStatus(id, status);
        }
        public BusinessResult<Contact> AddContact(Contact contact)
        {
            BusinessResult<Contact> result = new BusinessResult<Contact>();
            if (contact == null)
            {
                result.Errors.Add("Invalid contact object");
                return result;
            }
            bool isEmail = Regex.IsMatch(contact.Email, EmailRegex, RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                result.Errors.Add("Invalid email address");
            }
            if (result.Errors.Count == 0)
            {
                var id = _contactRepository.AddContact(contact);
                if (id > 0)
                {
                    contact.Id = id;
                    result.Value = contact;
                }

            }
            return result;
        }

        public int DeleteContact(int id)
        {
           return _contactRepository.DeleteContact(id);
        }
    }
}
