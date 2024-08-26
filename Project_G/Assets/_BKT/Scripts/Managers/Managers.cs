
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; } = false;

    private static Managers s_instance;
    private static Managers Instance { get { Init(); return s_instance; } }

    private ResourceManager _resource = new ResourceManager();
    private GameManager _game = new GameManager();
    private ObjectManager _obj = new ObjectManager();
    private MapManager _map = new MapManager();
    private ControllerManager _controller = new ControllerManager();
    private UIManager _ui = new UIManager();
    private PoolManager _pool = new PoolManager();
    private HeroSpawnAreaManager _heroSpawn = new HeroSpawnAreaManager();
    private RoundManager _round = new RoundManager();
    private SoundManager _sound = new SoundManager();
    // HSJ 
    private ScrapManager _scrap = new ScrapManager();
    private ProjectileManager _projectile = new ProjectileManager();
    private BaseMapManager _basemap = new BaseMapManager();
    private QuestManager _quest = new QuestManager();


    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static GameManager Game { get { return Instance?._game; } }
    public static ObjectManager Obj { get { return Instance?._obj; } }
    public static MapManager Map { get { return Instance?._map; } }
    public static ControllerManager Controller { get { return Instance?._controller; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static HeroSpawnAreaManager HeroSpawn { get { return Instance?._heroSpawn; } }
    public static RoundManager Round { get { return Instance?._round; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    // HSJ 
    public static ScrapManager Scrap { get { return Instance?._scrap; } }
    public static ProjectileManager Projectile { get { return Instance?._projectile; } }
    public static BaseMapManager BaseMap { get { return Instance?._basemap; } }
    public static QuestManager Quest { get { return Instance?._quest; } }

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
        }
    }
}
