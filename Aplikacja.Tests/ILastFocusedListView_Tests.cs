using NUnit.Framework;
using Aplikacja;

namespace Aplikacja.Tests
{
    [TestFixture]
    public class LastFocusedListViewShould
    {
        [Test]
        public void LastFocusedListViewFactory_ShouldReturnNull_IfInputIsNone()
        {
            var result = LastFocusedListViewFactory.Create(LastFocusedListView.None, null, null);

            Assert.IsNull(result);
        }

        [Test]
        public void LastFocusedListViewFactory_ShouldReturnAuthors_IfInputIsAuthors()
        {
            var result = LastFocusedListViewFactory.Create(LastFocusedListView.Authors, null, null);

            Assert.IsTrue(typeof(AuthorsLastFocused).IsInstanceOfType(result));
        }

        [Test]
        public void LastFocusedListViewFactory_ShouldReturnSongs_IfInputIsSongs()
        {
            var result = LastFocusedListViewFactory.Create(LastFocusedListView.Songs, null, null);

            Assert.IsTrue(typeof(SongsLastFocused).IsInstanceOfType(result));
        }
    }
}
