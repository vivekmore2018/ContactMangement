using CM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM.Interface.DataAccess;
using CM.Models;
using System.Data;
using Dapper;

namespace CM.DataAccess
{
    public class ContactRepository : IContactRepository
    {
        private IDbConnection _cn;
        public ContactRepository(IDbConnection cn)
        {
            _cn = cn;

        }
        public IEnumerable<Contact> GetAll()
        {
            return _cn.Query<Contact>(@"SELECT [Id] AS Id,
                                          [FName] AS FName,
                                          [LName] AS LName,
                                          [Email] AS Email,
                                          [PhoneNumber] AS PhoneNumber,
                                          CASE WHEN [Status]  = 'Y' THEN 'true' ELSE 'false' END AS Status
                                      FROM [dbo].[cm_contact]").ToList();

        }
        public Contact GetById(int id)
        {
            return _cn.Query<Contact>(@"SELECT [Id] AS Id,
                                          [FName] AS FName,
                                          [LName] AS LName,
                                          [Email] AS Email,
                                          [PhoneNumber] AS PhoneNumber,
                                          CASE WHEN [Status]  = 'Y' THEN 'true' ELSE 'false' END AS Status
                                      FROM [dbo].[cm_contact]
                                      WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }
        public int Update(Contact contact)
        {
            return _cn.Execute(@"UPDATE [dbo].[cm_contact] SET
                                [FName] = @FName,
                                [LName] = @LName,
                                [Email]= @Email,
                                [PhoneNumber]= @PhoneNumber,
                                [STATUS] = @Status 
                                 WHERE [Id]= @Id", new
            {
                FName = contact.FName,
                LName = contact.LName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Status = contact.Status ? 'Y' : 'N',
                Id = contact.Id
            });

        }
        public int ChangeStatus(int id, bool status)
        {
           return _cn.Execute(@"UPDATE [dbo].[cm_contact]                                 
                                [STATUS] = @Status 
                                 WHERE [Id]= @Id", new
            {
                Status = status ? 'Y' : 'N',
                Id = id
            });
        }
        public int AddContact(Contact contact)
        {
            string insertQuery = @"
                INSERT INTO [dbo].[cm_contact] ([FName],[LName],[Email],[PhoneNumber],[STATUS])
                VALUES (@FName,@LName,@Email,@PhoneNumber,@Status) 
                SELECT CAST(SCOPE_IDENTITY() as int)";
            return _cn.Query<int> (insertQuery, new
            {                
                FName = contact.FName,
                LName = contact.LName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Status = contact.Status ? 'Y' : 'N'
            }).Single();
        }
    }
}
