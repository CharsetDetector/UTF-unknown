using System.Collections.Generic;
using System.Linq;

namespace UtfUnknown
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

        public DetectionSummary(DetectionResult detectionResult)
        {
            AllDetectionResults = new List<DetectionResult> { detectionResult };
        }

        public DetectionResult Detected
        {
            get { return AllDetectionResults?.FirstOrDefault(); }
        }

        public IList<DetectionResult> AllDetectionResults { set; get; }
    }
}