using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF12RNGHelper
{
    class Chest
    {
        private int spawnChance;
        private int rngPosition;
        private int gilChance;
        private int itemChance;
        private int gilAmount;

        public Chest(int spawnchance, 
            int rngposition, 
            int gilchance, 
            int itemchance, 
            int gilamount)
        {
            initialize(spawnchance, 
                rngposition, 
                gilchance, 
                itemchance, 
                gilamount);
        }

        public Chest(string spawnchance,
            string rngposition,
            string gilchance,
            string itemchance,
            string gilamount)
        {
            initialize(int.Parse(spawnchance),
                int.Parse(rngposition),
                int.Parse(gilchance),
                int.Parse(itemchance),
                int.Parse(gilamount));
        }

        private void initialize(int spawnchance,
            int rngposition,
            int gilchance,
            int itemchance,
            int gilamount)
        {
            spawnChance = spawnchance;
            rngPosition = rngposition;
            gilChance = gilchance;
            itemChance = itemchance;
            gilAmount = gilamount;
        }

        public int getRNGPosition()
        {
            return rngPosition - 1;
        }

        public bool checkSpawn(uint PRNG)
        {
            return checkChest(PRNG, spawnChance);
        }

        public bool checkIfGil(uint PRNG)
        {
            return checkChest(PRNG, gilChance);
        }

        public bool checkIfFirstItem(uint PRNG)
        {
            return checkChest(PRNG, itemChance);
        }

        public int getGilAmount(uint PRNG)
        {
            return  1 + (int) (PRNG % gilAmount);
        }

        private double randToPercent(uint toConvert)
        {
            return toConvert % 100;
        }

        private bool checkChest(uint PRNG, double chance)
        {
            return randToPercent(PRNG) < chance;
        }
    }
}
