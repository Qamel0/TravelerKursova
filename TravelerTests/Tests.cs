using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Traveler.Controllers;
using Traveler.Interfaces;
using Traveler.Models.ViewModels;
using Traveler.Services;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Traveler.Models.Entities;
using System.Text;

namespace Traveler.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IHttpContextAccessor> _contextAccessorMock;
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            _controller = new AccountController(_mapperMock.Object, _userRepositoryMock.Object, _contextAccessorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public void Register_ReturnsViewResult_WhenCalled()
        {
            var result = _controller?.Register();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Register_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
            var model = new RegisterViewModel { Email = "test@example.com", Password = "Test1234" };
            _mapperMock.Setup(m => m.Map<User>(It.IsAny<RegisterViewModel>())).Returns(new User());
            _userRepositoryMock.Setup(u => u.AddUser(It.IsAny<User>())).Returns(true);

            var result = _controller?.Register(model);

            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void SignIn_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Error", "Test Error");

            var result = _controller?.SignIn(new RegisterViewModel());

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }
    }

    [TestFixture]
    public class StaysControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IStayRepository> _stayRepositoryMock;
        private Mock<IBytesConverterService<IFormFile>> _bytesConverterServiceMock;
        private StaysController _controller;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _stayRepositoryMock = new Mock<IStayRepository>();
            _bytesConverterServiceMock = new Mock<IBytesConverterService<IFormFile>>();

            _mapperMock.Setup(m => m.Map<Stay>(It.IsAny<StaysRegViewModel>())).Returns(new Stay());
            _stayRepositoryMock.Setup(s => s.AddStay(It.IsAny<Stay>())).Returns(true);
            _bytesConverterServiceMock.Setup(b => b.ConvertToBytes(It.IsAny<IFormFile>())).Returns(new byte[0]); // Мокируем сервис преобразования

            _controller = new StaysController(_mapperMock.Object, _stayRepositoryMock.Object, _bytesConverterServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public void NewPlace_ReturnsViewResult_WhenCalled()
        {
            var result = _controller.NewPlace();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }
    }

    [TestFixture]
    public class PhotoConverterToBytesTests
    {
        private Mock<IFormFile> _mockFormFile;
        private PhotoConverterToBytes _photoConverter;

        [SetUp]
        public void Setup()
        {
            _mockFormFile = new Mock<IFormFile>();
            _photoConverter = new PhotoConverterToBytes();
        }

        [Test]
        public void ConvertToBytes_ReturnsEmptyArray_WhenFileIsEmpty()
        {
            // Arrange
            var stream = new MemoryStream();
            _mockFormFile.Setup(f => f.OpenReadStream()).Returns(stream);
            _mockFormFile.Setup(f => f.Length).Returns(0);

            // Act
            var result = _photoConverter.ConvertToBytes(_mockFormFile.Object);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void ConvertToBytes_ThrowsArgumentNullException_WhenFileIsNull()
        {
            // Act & Assert
            Assert.That(() => _photoConverter.ConvertToBytes(default!),
                        Throws.ArgumentNullException);
        }
    }
}