namespace UtfUnknown.Core
{
    enum InputState
    {
        PureASCII=0,

        /// <summary>
        /// Found escape character or HZ "~{"
        /// </summary>
        EscASCII = 1,

        /// <summary>
        /// non-ascii byte (high-byte)
        /// </summary>
        Highbyte = 2
    };
}