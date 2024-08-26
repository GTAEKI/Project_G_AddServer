public static class Define
{
    #region Enum
    public enum EScene
    {
        TitleScene,
        BaseScene,
        BattleScene,
        GameClearScene,
        Dev_BKT,
        Test_BaseScene_HSJ,
        TestScene_HSJ,
    }

    public enum EObjectType
    {
        None,
        Creature,
        Building,
    }

    public enum ECreatureType
    {
        None,
        Hero,
        Enemy,
    }

    public enum EBuildingType
    {
        None,
        TargetBuilding,
        EnemyBuilding,
    }

    public enum ECreatureState
    {
        None,
        Idle,
        Move,
        Die,
    }

    public enum ECellCollisionType
    {
        None,
        Wall,
        Building,
        Enemy,
    }

    public enum EFindPathResult
    {
        Fail_LerpCell,
        Fail_NoPath,
        Fail_MoveTo,
        Success,
    }

    public enum EVirtualCamera
    {
        StartViewCamera,
        TopViewCamera,
        GameViewCamera,
    }

    public enum EColorType
    {
        None,
        White,
        Red,
        Yellow,
    }

    public enum EUIName 
    {
        MissionProgressBar,
    }

    public enum EHeroSpawnAreaName 
    {
        SpawnPoint0 = 0,
        SpawnPoint1 = 1, 
        SpawnPoint2 = 2,
        SpawnPoint3 = 3,
        SpawnPoint4 = 4,
        EndCount = 5,
    }

    public enum ESound
    {
        Bgm = 0,
        Effect = 1,
        EndCount = 2,
    }

    // HSJ
    public enum EPlacementBuildingType
    {
        Floor = 0,
        Building = 1
    }
    #endregion

    #region Hard Coding
    public const char MAP_TOOL_WALL = '0';
    public const char MAP_TOOL_NONE = '1';
    public const char MAP_TOOL_BUILDING = '2';
    public const char MAP_TOOL_Enemy = '3';

    public const string HeroRespawn = "HeroRespawn";
    public const string EnemyRespawn = "EnemyRespawn";
    public const string TargetBuilding = "TargetBuilding";
    public const string SpawnPoint = "SpawnPoint";
    //public const string TopViewCamera = "TopViewCamera";
    //public const string GameViewCamera = "GameViewCamera";

    public const float UpdateStateTick = 0.02f;
    #endregion
}