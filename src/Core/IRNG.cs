using System;

namespace FF12RNGHelper.Core
{
    public interface IRNG : IDeepCloneable<IRNG>
    {
        void sgenrand();
        void sgenrand(UInt32 s);
        UInt32 genrand();

        RNGState saveState();
        void loadState(int inmti, UInt32[] inmt, int position);
        void loadState(RNGState state);
        int getPosition();
    }

    /// <summary>
    /// Represents the state of the Mersenne Twister RNG
    /// </summary>
    public struct RNGState
    {
        /// <summary>
        /// Index into the mt state array.
        /// </summary>
        public int mti;

        /// <summary>
        /// The mt state array.
        /// </summary>
        public UInt32[] mt;

        public int position;

    }
}
