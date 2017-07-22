using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF12RNGHelper
{
    /// <summary>
    /// This interface is for objects that can be deep copied.
    /// </summary>
    public interface IDeepCloneable
    {
        /// <summary>
        /// Perform a deep copy and return the copy.
        /// </summary>
        object DeepClone();
    }

    /// <summary>
    /// Templated interface for objects that can be deep copied
    /// </summary>
    public interface IDeepCloneable<T> : IDeepCloneable
    {
        /// <summary>
        /// Perform a deep copy and return the copy.
        /// </summary>
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        T DeepClone();
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}
