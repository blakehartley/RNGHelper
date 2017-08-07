namespace FF12RNGHelper.Core
{
    /// <summary>
    /// This interface exposes the necessary public methods
    /// required to use RngHelper objects
    /// 
    /// Usage order:
    /// 0) Construction
    /// 1) FindFirstRngPosition(...)
    /// 2) FindNextRngPosition(...) or ConsumeNextNRngPositions(...)
    /// 3) CalculateRng(...)
    /// 4) GetChestFutureRng()
    /// 5) GetNextExpecteHealValue() and GetAttacksUntilNextCombo()
    /// </summary>
    public interface IRngHelper
    {
        #region fatty interface methods

        // C# doesn't support return type covariance, so we will
        // instead provide a fatty interface which must enumerate
        // all implementors of IFutureRng we wish to return.
        /// <summary>
        /// After calculating RNG, call this method to get the
        /// ChestFutureRng object
        /// </summary>
        ChestFutureRng GetChestFutureRng();

        #endregion fatty interface methods

        #region public methods

        /// <summary>
        /// Reset object to initial state.
        /// </summary>
        void Reinitialize();

        /// <summary>
        /// Starts a new search for RNG position
        /// This method MUST be called successfully before using
        /// FindNextRngPosition
        /// </summary>
        /// <param name="firstHealValue">Value of the first heal spell</param>
        /// <returns>True if first RNG position found, false otherwise</returns>
        bool FindFirstRngPosition(int firstHealValue);

        /// <summary>
        /// Given a new heal value, locate the next position
        /// in the RNG list that matches all of our previous
        /// values
        /// This method MUST only be called after a successful call to
        /// FindFirstRngPosition
        /// </summary>
        /// <param name="nextHealValue">Value of the next heal spell</param>
        /// <returns>True if next RNG position found, false otherwise</returns>
        bool FindNextRngPosition(int nextHealValue);

        /// <summary>
        /// Consume the next N RNG positions
        /// </summary>
        bool ConsumeNextNRngPositions(int positionsToConsume);

        /// <summary>
        /// Given a start and end value, calculate the future RNG
        /// </summary>
        /// <param name="rowsToRender"></param>
        void CalculateRng(int rowsToRender);

        /// <summary>
        /// Returns what the next heal value is going to be based
        /// on the current RNG position.
        /// </summary>
        /// <returns>Next expected heal value</returns>
        int GetNextExpectedHealValue();

        /// <summary>
        /// How many attacks away from a combo are we using the current
        /// weapon
        /// </summary>
        /// <returns>Number of attacks before next combo, 
        /// -1 if not found</returns>
        int GetAttacksUntilNextCombo();

        #endregion public methods
    }
}