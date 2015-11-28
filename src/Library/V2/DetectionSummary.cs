using System.Collections.Generic;
using System.Linq;

namespace Ude
{
    public class DetectionSummary
    {
        /// <summary>
        /// Empty
        /// </summary>
        public DetectionSummary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DetectionSummary(IList<DetectionResult> allDetectionResults)
        {
            AllDetectionResults = allDetectionResults;
        }

        public DetectionSummary(DetectionResult allDetectionResults)
        {
            AllDetectionResults = new List<DetectionResult> { allDetectionResults };
        }

        public DetectionResult Detected
        {
            get { return AllDetectionResults?.FirstOrDefault(); }
        }

        public IList<DetectionResult> AllDetectionResults { set; get; }
    }
}