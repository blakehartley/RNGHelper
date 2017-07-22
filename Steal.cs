using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF12RNGHelper
{
    class Steal
    {
		private int commonChance = 55;
		private int uncommonChance = 10;
		private int rareChance = 3;
		private int commonChanceCuffs = 80;
		private int uncommonChanceCuffs = 30;
		private int rareChanceCuffs = 6;

		public Steal()
        {
			
		}

        public string checkSteal(uint PRNG0, uint PRNG1, uint PRNG2)
        {
			if ( randToPercent(PRNG0) < rareChance )
			{
				return "Rare";
			}
			else if ( randToPercent(PRNG1) < uncommonChance )
			{
				return "Uncommon";
			}
			else if ( randToPercent(PRNG2) < commonChance )
			{
				return "Common";
			}
			return "None";
        }

		public string checkStealCuffs(uint PRNG0, uint PRNG1, uint PRNG2)
		{
			string returnStr = "";

			if (randToPercent(PRNG0) < rareChanceCuffs )
			{
				returnStr += "Rare";
			}
			if ( randToPercent(PRNG1) < uncommonChanceCuffs )
			{
				returnStr += " + Uncommon";
			}
			if ( randToPercent(PRNG2) < commonChanceCuffs )
			{
				returnStr += " + Common";
			}
			if (returnStr == "")
			{
				returnStr = "None";
			}
			return returnStr.TrimStart(new char[] { ' ', '+' });
		}

        private double randToPercent(uint toConvert)
        {
            return (int) (toConvert % 100);
        }
    }
}
