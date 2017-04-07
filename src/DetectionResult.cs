using System;
using System.Text;
using UtfUnknown.Core;

namespace UtfUnknown
{
    /// <summary>
    /// Result of a detection
    /// </summary>
    public class DetectionResult
    {
        /// <summary>
        /// New result
        /// </summary>
        public DetectionResult(string encodingShortName, float confidence, CharsetProber prober = null, TimeSpan? time = null)
        {
            EncodingName = encodingShortName;
            Confidence = confidence;

            try
            {
                Encoding = System.Text.Encoding.GetEncoding(encodingShortName);
            }
            catch (Exception)
            {
                
               //wrong name
            }
         
            Prober = prober;
            Time = time;
        }

        /// <summary>
        /// New Result
        /// </summary>
        public DetectionResult(CharsetProber prober, TimeSpan? time = null) 
            : this(prober.GetCharsetName(), prober.GetConfidence(), prober, time)
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
        /// The confidence of the found encoding
        /// </summary>
        public float Confidence { get; set; }

        /// <summary>
        /// The used prober for detection
        /// </summary>
        public CharsetProber Prober { get; set; }

        /// <summary>
        /// The time spent
        /// </summary>
        public TimeSpan? Time { get; set; }

    }
}
