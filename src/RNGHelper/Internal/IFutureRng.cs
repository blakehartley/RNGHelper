namespace FF12RNGHelper
{
    /// <summary>
    /// This class encapsulates the methods
    /// required to use a FutureRng object.
    /// TO-DO: Remove if no similarites found.
    /// </summary>
    internal interface IFutureRng
    {
        /// <summary>
        /// How far into the future did we calculate
        /// </summary>
        int GetTotalFutureRngPositions();
    }
}