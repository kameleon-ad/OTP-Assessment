using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OTP_Assessment.Tests
{
    [TestClass]
    public class Email_OPT_Module_Tests
    {
        private Email_OPT_Module _module;

        [TestInitialize]
        public void TestInitialize()
        {
            _module = new Email_OPT_Module("peter.james.ds@outlook.com", "Test!233");
            _module.Start();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _module.Close();
        }

        [TestMethod]
        public void GenerateOPTEmail_ShouldReturn_OK_WhenGivenValidEmail()
        {
            string validEmail = "gribnekov.jeong@outlook.com";

            // Act
            var result = _module.GenerateOPTEmail(validEmail);

            // Assert
            Assert.AreEqual(Email_OPT_Module.EmailStatus.STATUS_EMAIL_OK, result);
        }

        [TestMethod]
        public void GenerateOPTEmail_ShouldReturn_INVALID_WhenGivenInvalidEmail()
        {
            var invalidEmail = "invalid-email-address";

            // Act
            var result = _module.GenerateOPTEmail(invalidEmail);

            // Assert
            Assert.AreEqual(Email_OPT_Module.EmailStatus.STATUS_EMAIL_INVALID, result);
        }

        [TestMethod]
        public void CheckOTP_ShouldReturn_OK_WhenGivenValidOTP()
        {
            string validEmail = "gribnekov.jeong@outlook.com";
            _module.GenerateOPTEmail(validEmail);

            // Arrange
            var input = new MemoryStream();
            var writer = new StreamWriter(input);
            writer.WriteLine(_module.getOTP());
            writer.Flush();
            input.Seek(0, SeekOrigin.Begin);

            // Act
            var result = _module.CheckOTP(input).Result;

            // Assert
            Assert.AreEqual(Email_OPT_Module.OTPStatus.STATUS_OTP_OK, result);
        }

        [TestMethod]
        public void CheckOTP_ShouldReturn_TIMEOUT_WhenGivenExpiredOTP()
        {
            string validEmail = "gribnekov.jeong@outlook.com";
            _module.GenerateOPTEmail(validEmail);

            // Arrange
            var input = new MemoryStream();
            var writer = new StreamWriter(input);
            writer.WriteLine(_module.getOTP());
            writer.Flush();
            input.Seek(0, SeekOrigin.Begin);

            // Wait for OTP to expire
            System.Threading.Thread.Sleep(61000);

            // Act
            var result = _module.CheckOTP(input).Result;

            // Assert
            Assert.AreEqual(Email_OPT_Module.OTPStatus.STATUS_OTP_TIMEOUT, result);
        }

        [TestMethod]
        public void CheckOTP_ShouldReturn_FAIL_WhenGivenInvalidOTP()
        {
            string validEmail = "gribnekov.jeong@outlook.com";
            _module.GenerateOPTEmail(validEmail);

            // Arrange
            var input = new MemoryStream();
            var writer = new StreamWriter(input);
            writer.WriteLine("invalid-otp-code");
            writer.Flush();
            input.Seek(0, SeekOrigin.Begin);

            // Act
            var result = _module.CheckOTP(input).Result;

            // Assert
            Assert.AreEqual(Email_OPT_Module.OTPStatus.STATUS_OTP_FAIL, result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
