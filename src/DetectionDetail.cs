using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UtfUnknown.Core;
using UtfUnknown.Core.Probers;

[assembly: InternalsVisibleTo("UtfUnknown.Tests, PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010029f6b4defac763" +
"66721687460b44b7619e8e19a411f785279316fdae2f6965edfa4a460304fe8b4ed796d5356a1c" +
"225131b9087983d9ff9530df9307eab17d88cd4f1005a45f6f35523445d1ff7323322f3060cffc" +
"0d70d0cb1b4b7d46081bbead31844927aaadb0508b64bf298de5abe5ea5cca8b92490c961b7b75" +
"13c2c2a9")]
namespace UtfUnknown
{
    /// <summary>
    /// Detailed result of a detection
    /// </summary>
    public class DetectionDetail
    {
        /// <summary>
        /// A dictionary for replace unsupported codepage name in .NET to the nearly identical version.
        /// </summary>
        private static readonly Dictionary<string, string> FixedToSupportCodepageName =
            new Dictionary<string, string>
            {
                // CP949 is superset of ks_c_5601-1987 (see https://github.com/CharsetDetector/UTF-unknown/pull/74#issuecomment-550362133)
                {CodepageName.CP949, CodepageName.KS_C_5601_1987},
                {CodepageName.ISO_2022_CN, CodepageName.X_CP50227},
            };

        /// <summary>
        /// New result
        /// </summary>
        public DetectionDetail(string encodingShortName, float confidence, CharsetProber prober = null,
            TimeSpan? time = null, string statusLog = null)
        {
            EncodingName = encodingShortName;
            Confidence = confidence;
            Encoding = GetEncoding(encodingShortName);
            Prober = prober;
            Time = time;
            StatusLog = statusLog;
        }

        /// <summary>
        /// New Result
        /// </summary>
        public DetectionDetail(CharsetProber prober, TimeSpan? time = null)
            : this(prober.GetCharsetName(), prober.GetConfidence(), prober, time, prober.DumpStatus())
        {
        }

        /// <summary>
        /// The (short) name of the detected encoding. For full details, check <see cref="Encoding"/>
        /// </summary>
        public string EncodingName { get; }

        /// <summary>
        /// The detected encoding. 
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// The confidence of the found encoding. Between 0 and 1.
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// The used prober for detection
        /// </summary>
        public CharsetProber Prober { get; set; }

        /// <summary>
        /// A Byte Order Mark was detected
        /// </summary>
        public bool HasBOM { get; set; }

        /// <summary>
        /// The time spend
        /// </summary>
        public TimeSpan? Time { get; set; }

        public string StatusLog { get; set; }

        public override string ToString()
        {
            return $"Detected {EncodingName} with confidence of {Confidence}. (BOM: {HasBOM})";
        }

        internal static Encoding GetEncoding(string encodingShortName)
        {
            var encodingName = FixedToSupportCodepageName.TryGetValue(encodingShortName, out var supportCodepageName)
                ? supportCodepageName
                : encodingShortName;
            try
            {
                return Encoding.GetEncoding(encodingName);
            }
            catch (Exception exception) when
                (exception is ArgumentException || // unsupported name
                exception is NotSupportedException)
            {
#if NETSTANDARD && !NETSTANDARD1_0 || NETCOREAPP3_0
                return CodePagesEncodingProvider.Instance.GetEncoding(encodingName);
#else
                return null;
#endif
            }
        }
    }
}