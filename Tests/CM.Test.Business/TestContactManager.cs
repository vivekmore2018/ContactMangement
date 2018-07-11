using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CM.Interface.DataAccess;
using CM.Business;
using CM.Models;
using CM.Interface.Business;

namespace CM.Test.Business
{
    [TestClass]
    public class TestContactManager
    {
        private Mock<IContactRepository> mockIContactRepository;
        private ContactManager contactManager;

        [TestInitialize]
        public void Setup()
        {
            mockIContactRepository = new Mock<IContactRepository>();
            contactManager = new ContactManager(mockIContactRepository.Object);
        }
        [TestMethod]
        public void TestInvalidEmail_AddContact()
        {
            //Arrange
            var inputContact = new Contact() { Email = "dd", FName = "fname", LName = "lastname", PhoneNumber = "test", Status = true };

            //mockOrgManager.Setup(x => x.GetOrgAttributesFeatureMatrix(It.IsAny<int>())).Returns(() => MockAttibuteDataProvider.GetOrgAttributesFeatureMatrix_Success());
            mockIContactRepository.Setup(x => x.AddContact(It.IsAny<Contact>())).Returns(2);

            //Act
            var result = contactManager.AddContact(inputContact);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BusinessResult<Contact>));
            Assert.AreEqual(result.Status, Status.Failure);
            Assert.AreEqual(1, result.Errors.Count);

        }
        [TestMethod]
        public void TestSucessAddContact()
        {
            //Arrange
            var inputContact = new Contact() { Email = "dd@dd.com", FName = "fname", LName = "lastname", PhoneNumber = "test", Status = true };

            //mockOrgManager.Setup(x => x.GetOrgAttributesFeatureMatrix(It.IsAny<int>())).Returns(() => MockAttibuteDataProvider.GetOrgAttributesFeatureMatrix_Success());
            mockIContactRepository.Setup(x => x.AddContact(It.IsAny<Contact>())).Returns(2);

            //Act
            var result = contactManager.AddContact(inputContact);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BusinessResult<Contact>));
            Assert.AreEqual(result.Status, Status.Success);
            Assert.AreEqual(0, result.Errors.Count);
            Assert.AreEqual(result.Value.Id, 2);

        }
    }
}
