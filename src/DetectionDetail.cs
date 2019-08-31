using System;
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
        /// New result
        /// </summary>
        public DetectionDetail(
            string encodingShortName,
            float confidence,
            CharsetProber prober = null,
            TimeSpan? time = null,
            string statusLog = null)
        {
            EncodingName = encodingShortName.Split('(').First().Trim();
            Confidence = confidence;

            try
            {
                var encodingName = encodingShortName
                    .Split('(').Last()
                    .Split(')').First()
                    .Trim();
                Encoding = Encoding.GetEncoding(encodingName);
            }
            catch (Exception)
            {
               //wrong name
            }
         
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
    }
}