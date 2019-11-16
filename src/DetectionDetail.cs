using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtfUnknown.Core;
using UtfUnknown.Core.Probers;

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
        /// The time spend
        /// </summary>
        public TimeSpan? Time { get; set; }

        public string StatusLog { get; set; }

        public override string ToString()
        {
            return $"Detected {EncodingName} with confidence of {Confidence}";
        }

        private static Encoding GetEncoding(string encodingShortName)
        {
            try
            {
                var encodingName = encodingShortName;
                if (FixedToSupportCodepageName.TryGetValue(encodingShortName, out var supportCodepageName))
                {
                    encodingName = supportCodepageName;
                }

                return Encoding.GetEncoding(encodingName);
            }
            catch (Exception)
            {
                //wrong name
                return null;
            }
        }
    }
}