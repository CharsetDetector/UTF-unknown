using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UtfUnknown.Core;

namespace UtfUnknown.Tests
{
    public class CodepageNameTest
    {
        [TestCaseSource(nameof(_encodingNames))]
        public void DetectionDetailGetEncodingNotException(string codepageName)
        {
            var encoding = DetectionDetail.GetEncoding(codepageName);
            Assert.IsNotNull(encoding);
        }
        
        private static IReadOnlyList<string> _encodingNames = typeof(CodepageName)
            .GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.CreateInstance)
            .Select(x => x.GetValue(null).ToString())
            .ToList();
    }
}