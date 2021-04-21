using NUnit.Framework;
using Aplikacja;
using System.Collections.Generic;

namespace Aplikacja.Tests
{
    [TestFixture]
    public class AddSongDialogShould
    {
        [TestCase(null)]
        [TestCase("")]
        public void DirectoryIsSet_ShouldReturnFalse_ForNotValidInput(object data)
        {
            var sut = new DirectoryIsSet();
            var result = sut.Validate(data, null);
            Assert.IsFalse(result.IsValid);
        }

        [TestCase("C://my/path/to")]
        public void DirectoryIsSet_ShouldReturnTrue_ForValidInput(object data)
        {
            var sut = new DirectoryIsSet();
            var result = sut.Validate(data, null);
            Assert.IsTrue(result.IsValid);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TitleIsSet_ShouldReturnFalse_ForNotValidInput(object data)
        {
            var sut = new TitleIsSet();
            var result = sut.Validate(data, null);
            Assert.IsFalse(result.IsValid);
        }

        [TestCase("My song title")]
        public void TitleIsSet_ShouldReturnTrue_ForValidInput(object data)
        {
            var sut = new TitleIsSet();
            var result = sut.Validate(data, null);
            Assert.IsTrue(result.IsValid);
        }
    }
}
