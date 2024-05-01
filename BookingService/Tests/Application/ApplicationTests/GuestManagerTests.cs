using Application;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
/*    class FakeRepo : IGuestRepository
    {
        public Task<int> Create(Guest guest)
        {
            return Task.FromResult(111);
        }

        public Task<Guest> Get(int id)
        {
            throw new NotImplementedException();
        }
    }*/
    public class Tests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
            //var fakeRepo = new FakeRepo();
            //guestManager = new GuestManager(fakeRepo);
        }

        [Test]
        public async Task HappyPath()
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulado",
                Surname = "De Tal",
                Email = "fulano@gmail.com",
                IdNumber = "abca",
                IdTypeCode = 1
            };

            int expectedId = 222;

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            Assert.AreEqual(res.Data.Name, guestDto.Name);

            Assert.AreEqual(res.Data.Surname, guestDto.Surname);
            Assert.AreEqual(res.Data.Email, guestDto.Email);
            Assert.AreEqual(res.Data.IdNumber, guestDto.IdNumber);
            Assert.AreEqual(res.Data.IdTypeCode, guestDto.IdTypeCode);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulado",
                Surname = "De Tal",
                Email = "fulano@gmail.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };


            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The ID passed is not valid");
        }

        [TestCase("", "SurnameTest", "abcd@email.com")]
        [TestCase(null, "SurnameTest", "abcd@email.com")]
        [TestCase("Fulano", "", "abcd@email.com")]
        [TestCase("Fulano", null, "abcd@email.com")]
        [TestCase("Fulano", "SurnameTest", "")]
        [TestCase("Fulano", "SurnameTest", null)]
        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string name, string surname, string email)
        {
            var guestDto = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };


            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }

        [TestCase("b@b.com")]
        [TestCase("emailsemarrobaesemponto")]
        public async Task Should_Return_InvalidEmailExceptions_WhenEmailAreInvalid(string email)
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulado",
                Surname = "De Tal",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };


            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.INVALID_EMAIL);
            Assert.AreEqual(res.Message, "The given email is not value");
        }
    }
}