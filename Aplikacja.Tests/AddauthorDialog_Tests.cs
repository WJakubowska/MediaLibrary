using NUnit.Framework;
using Aplikacja;
using System.Collections.Generic;

namespace Aplikacja.Tests
{
    [TestFixture]
    public class AddAuthorDialogShould
    {
        public class AuthorUniquenessCheckerMock : IAuthorUniquenessChecker
        {
            public List<string> AuthorsInDb { get; set; }

            public AuthorUniquenessCheckerMock(List<string> authors)
            {
                AuthorsInDb = authors;
            }

            public bool IsAuthorNameUnique(string authorName)
            {
                return !AuthorsInDb.Contains(authorName);
            }
        }

        AuthorUniquenessCheckerMock authorUniquenessCheckerMock;

        [SetUp]
        public void Init()
        {
            authorUniquenessCheckerMock = new AuthorUniquenessCheckerMock(new List<string>(){"author1"});
        }

        [TestCase(null)]
        [TestCase(4)]
        [TestCase("")]
        [TestCase("author1")]
        [TestCase("fo")]
        public void AuthorNameValidation_ShouldReturnFalse_ForNotValidInputs(object data)
        {
            var sut = new AuthorNameValidation(authorUniquenessCheckerMock);
            var result = sut.Validate(data, null);
            Assert.IsFalse(result.IsValid);
        }

        [TestCase("author2")]
        [TestCase("example authors name")]
        public void AuthorNameValidation_ShouldReturnTrue_ForValidInput(object data)
        {
            var sut = new AuthorNameValidation(authorUniquenessCheckerMock);
            var result = sut.Validate(data, null);
            Assert.IsTrue(result.IsValid);
        }
    }
}
