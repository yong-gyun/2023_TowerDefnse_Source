public class Define
{
    public const float TILE_SIZE = 2.2F;

    public enum UIEvent
    {
        Click,
        PointerEnter,
        PointerExit
    }

    public enum Item
    {
        Unknow,
        TowerHeal,
        EnemySlow,
        Gold,
        TowerAttackSpeedUp,
        EnemyAttackStop,
        SummonPatrolUnit
    }

    public enum State
    {
        Idle,
        Move,
        Attack,
        Rush,
        Hit,
        Die,
    }

    public enum Priority
    {
        Illusion,
        ProtectedTower,
        Tower
    }

    public enum WorldObject
    {
        Unit,
        Tower,
    }

    public enum Unit
    {
        Melee,
        RangedAttck,
        QuickMove,
        Flyable,
        Gold,
        MiddleBoss,
        StageBoss
    }

    public enum Scene
    {
        Menu,
        Stage1,
        Stage2,
        Rank
    }

    public enum Tower
    {
        Default,
        Multiply,
        Focus,
        Illusion,
        Obstacle,
        Protected,
        LastProtected
    }
}
