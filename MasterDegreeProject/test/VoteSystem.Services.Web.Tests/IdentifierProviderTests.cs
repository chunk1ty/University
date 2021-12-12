using NUnit.Framework;
using VoteSystem.Services.Web.Contracts;

namespace VoteSystem.Services.Web.Tests
{
    [TestFixture]
    public class IdentifierProviderTests
    {
        [Test]
        public void EncodingAndDecodingDoesntChangeTheId()
        {
            const int Id = 17;
            IIdentifierProvider provider = new IdentifierProvider();
            var encoded = provider.EncodeId(Id);

            var actual = provider.DecodeId(encoded);

            Assert.AreEqual(Id, actual);
        }
    }
}
