namespace UtfUnknown.Core.Probers
{
    public enum ProbingState
    {
        /// <summary>
        /// No sure answer yet, but caller can ask for confidence
        /// </summary>
        Detecting = 0, // 
        /// <summary>
        /// Positive answer
        /// </summary>
        FoundIt = 1,
        /// <summary>
        /// Negative answer 
        /// </summary>
        NotMe = 2
    };
}