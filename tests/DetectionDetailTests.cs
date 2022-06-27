using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UtfUnknown;
using UtfUnknown.Core;
using UtfUnknown.Core.Probers;

namespace UtfUnknown.Tests
{
    [TestFixture]
    public class DetectionDetailTests
    {

        [TestCaseSource(nameof(EncodingNames))]
        public void DetectionDetailGetEncodingIsNotNull(string codepageName)
        {
            var encoding = DetectionDetail.GetEncoding(codepageName);
            Assert.IsNotNull(encoding);
        }

        private static readonly HashSet<string> UnsupportedEncodings = new HashSet<string>
        {
            CodepageName.ISO_8859_10,
            CodepageName.ISO_8859_16,
            CodepageName.EUC_TW,
            CodepageName.VISCII,
            CodepageName.X_ISO_10646_UCS_4_2143,
            CodepageName.X_ISO_10646_UCS_4_3412,
        };

        private static readonly IReadOnlyList<string> EncodingNames = typeof(CodepageName)
            .GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.CreateInstance)
            .Select(x => x.GetValue(null).ToString())
            .Where(x => !UnsupportedEncodings.Contains(x))
            .ToList();


        [Test]
        public void GetEncodingShouldHandleIncorrectEncoding()
        {
            // Arrange
            string encoding = "wrong";
            // Act
            var result = DetectionDetail.GetEncoding(encoding);

            // Assert
            Assert.AreEqual(null, result);
        }
    }
}
