using System;

namespace FF12RNGHelper
{
    public class Chest
    {
        private int _spawnChance;
        private int _rngPosition;
        private int _gilChance;
        private int _itemChance;
        private int _gilAmount;
        private bool _wantItem1;

        // chest finding state
        private bool _chestSpawn;

        private bool _chestFound;
        private int _chestFoundPos;
        private int _chestItemPos;

        [Obsolete]
        public Chest(int spawnchance,
            int rngposition,
            int gilchance,
            int itemchance,
            int gilamount)
        {
            Initialize(spawnchance,
                rngposition,
                gilchance,
                itemchance,
                gilamount);
        }

        public Chest(int spawnchance,
            int rngposition,
            int gilchance,
            int itemchance,
            int gilamount,
            bool wantitem1)
        {
            Initialize(spawnchance,
                rngposition,
                gilchance,
                itemchance,
                gilamount,
                wantitem1);
        }

        [Obsolete]
        public Chest(string spawnchance,
            string rngposition,
            string gilchance,
            string itemchance,
            string gilamount)
        {
            Initialize(int.Parse(spawnchance),
                int.Parse(rngposition),
                int.Parse(gilchance),
                int.Parse(itemchance),
                int.Parse(gilamount));
        }

        private void Initialize(int spawnchance,
            int rngposition,
            int gilchance,
            int itemchance,
            int gilamount,
            bool wantitem1 = false)
        {
            _spawnChance = spawnchance;
            _rngPosition = rngposition;
            _gilChance = gilchance;
            _itemChance = itemchance;
            _gilAmount = gilamount;
            _wantItem1 = wantitem1;

            InitializeSpawnInfo();
        }

        private void InitializeSpawnInfo()
        {
            _chestSpawn = false;
            _chestFound = false;
            _chestFoundPos = -1;
            _chestItemPos = -1;
        }

        public int getRNGPosition()
        {
            return _rngPosition - 1;
        }

        public bool checkSpawn(uint prng)
        {
            return CheckChest(prng, _spawnChance);
        }

        public bool checkIfGil(uint prng)
        {
            return CheckChest(prng, _gilChance);
        }

        public bool checkIfFirstItem(uint prng)
        {
            return CheckChest(prng, _itemChance);
        }

        public int getGilAmount(uint prng)
        {
            return 1 + (int) (prng % _gilAmount);
        }

        public bool WantItemOne()
        {
            return _wantItem1;
        }

        public void ResetSpawnInfo()
        {
            InitializeSpawnInfo();
        }

        public int GetChestFoundPosition()
        {
            return _chestFoundPos;
        }

        public void SetChestFoundPosition(int position)
        {
            _chestFoundPos = position;
        }

        public bool HasChestSpawned()
        {
            return _chestSpawn;
        }

        public void SetChestSpawned()
        {
            _chestSpawn = true;
        }

        public bool IsChestFound()
        {
            return _chestFound;
        }

        public void SetChestFound()
        {
            _chestFound = true;
        }

        public int GetChestItemPosition()
        {
            return _chestItemPos;
        }

        public void SetChestItemPosition(int position)
        {
            _chestItemPos = position;
        }

        private double RandToPercent(uint toConvert)
        {
            return toConvert % 100;
        }

        private bool CheckChest(uint prng, double chance)
        {
            return RandToPercent(prng) < chance;
        }
    }
}
