// Enum 관리 클래스
public class Define
{
    public enum Role
    {
        None,
        Player,
        NormalEnemy,
        BossEnemy,
    }

    // Behavior Tree 노드 상태
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public enum NormalMonsterType
    {
        Knight,
        Archer,
        Wizard,
        Necromancer,
        Orc
    }

    public enum  SceneType
    {
        TitleScene,
        InGameScene,
    }
}