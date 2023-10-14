namespace Data
{
    public class StatData
    {
        public float maxHp;
        public float hp;
        public float attack;
        public float attackSpeed;
        public float attackRange;
    }

    public class TowerStatData : StatData
    {
        public int size;
        public int price;

        public TowerStatData()
        {

        }

        public TowerStatData(float maxHp, float hp, float attack, float attackSpeed, float attackRange, int size, int price)
        {
            this.maxHp = maxHp;
            this.hp = hp;
            this.attack = attack;
            this.attackRange = attackRange;
            this.attackSpeed = attackSpeed;
            this.size = size;
            this.price = price;
        }
    }

    public class UnitStatData : StatData
    {
        public float moveSpeed;
        public int rewardGold;
        public int rewardScore;

        public UnitStatData()
        {

        }

        public UnitStatData(float maxHp, float hp, float attack, float attackRange, float attackSpeed, float moveSpeed, int rewardGold, int rewardScore)
        {
            this.maxHp = maxHp;
            this.hp = hp;
            this.attack = attack;
            this.attackRange = attackRange;
            this.attackSpeed = attackSpeed;
            this.moveSpeed = moveSpeed;
            this.rewardGold = rewardGold;
            this.rewardScore = rewardScore;
        }
    }
}
