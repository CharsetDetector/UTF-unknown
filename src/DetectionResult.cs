
using System.Collections.Generic;


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
        public DetectionDetail Detected 
        {
            get
            {
                if (Details == null || Details.Count < 1)
                    return null;
                
                return Details[0];
            }
           
        }
        
        
        /// <summary>
        /// All results
        /// </summary>
        public IList<DetectionDetail> Details { get; set; }

        public override string ToString()
        {
            string[] details = new string[Details?.Count ?? 0];
            for (int i = 0; i < details.Length; ++i)
            {
                details[i] = Details[i].ToString();
            }
            
            return $"{nameof(Detected)}: {Detected}, \n{nameof(Details)}:\n - {string.Join("\n- ", details)}";
        }
        
        
    }
}