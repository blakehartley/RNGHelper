namespace FF12RNGHelper.Core
{
    public class Monster
    {
        public static long RareSpawnValue = 4294967296;

        private readonly double _minChanceFraction;
        private readonly double _maxChanceFraction;
        private readonly int _rngPosition;

        // Monster state
        private bool _spawned;
        private int _foundPosition;

        public Monster(double minChance, double maxChance, int rngPosition)
        {
            const int toFraction = 100;

            _minChanceFraction = minChance / toFraction;
            _maxChanceFraction = maxChance / toFraction;
            _rngPosition = rngPosition;

            Initialize();
        }

        private void Initialize()
        {
            _spawned = false;
            _foundPosition = -1;
        }

        public void ResetSpawnInfo()
        {
            Initialize();
        }

        public bool HasMonsterSpawned()
        {
            return _spawned;
        }

        public void SetMonsterSpawned()
        {
            _spawned = true;
        }

        public void SetMonsterFoundPosition(int foundPosition)
        {
            _foundPosition = foundPosition;
        }

        public int GetMonsterFoundPosition()
        {
            return _foundPosition;
        }

        public bool SpawnCheck(double chance)
        {
            return chance > _minChanceFraction && chance < _maxChanceFraction;
        }

        public int GetRngPosition()
        {
            return _rngPosition;
        }
    }
}