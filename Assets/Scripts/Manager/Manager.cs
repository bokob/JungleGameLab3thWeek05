using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    public static Manager Instance => _instance;

    #region 매니저
    GameManager _game = new GameManager();
    InputManager _input = new InputManager();
    DataManager _data = new DataManager();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    ResourceManager _resource = new ResourceManager();

    public static GameManager Game => Instance._game;
    public static InputManager Input => Instance._input;
    public static DataManager Data => Instance._data;
    public static SoundManager Sound => Instance._sound;
    public static UIManager UI => Instance._ui;
    public static ResourceManager Resource => Instance._resource;
    #endregion

    #region 테스트
    [Header("테스트")]
    [SerializeField] List<Transform> _enemyList = new List<Transform>();
    public List<Transform> EnemyList => _enemyList;

    #endregion

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    // 각 매니저 초기화
    public void Init()
    {
        Game.Init();
        Input.Init();
        Data.Init();
        Sound.Init();
        UI.Init();
    }

    public void OnDestroy()
    {
        Input.Clear();
    }
}