using System;
using System.Collections.Generic;
using System.Linq;

namespace UtfUnknown
{
    /// <summary>
    /// Result of a detection.
    /// </summary>
    public class DetectionResult
    {
        /// <summary>
        /// Empty
        /// </summary>
        public DetectionResult()
        {
        }

        /// <summary>
        /// Multiple results
        /// </summary>
        public DetectionResult(IList<DetectionDetail> details)
        {
            Details = details;
        }

        /// <summary>
        /// Single result
        /// </summary>
        /// <param name="detectionDetail"></param>
        public DetectionResult(DetectionDetail detectionDetail)
        {
            Details = new List<DetectionDetail> { detectionDetail };
        }

        /// <summary>
        /// Get the best Detection
        /// </summary>
        public DetectionDetail Detected => Details?.FirstOrDefault();

        /// <summary>
        /// All results
        /// </summary>
        public IList<DetectionDetail> Details { get; set; }

        public override string ToString()
        {
            return $"{nameof(Detected)}: {Detected}, \n{nameof(Details)}:\n - {string.Join("\n- ", Details?.Select(d => d.ToString()))}";
        }
    }
}