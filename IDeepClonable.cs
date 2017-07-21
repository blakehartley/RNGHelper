using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF12RNGHelper
{
    public interface IDeepCloneable
    {
        object DeepClone();
    }

    public interface IDeepCloneable<T> : IDeepCloneable
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        T DeepClone();
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}
