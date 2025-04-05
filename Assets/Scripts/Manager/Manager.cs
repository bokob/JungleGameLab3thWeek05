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
    SceneManagerEX _scene = new SceneManagerEX();

    public static GameManager Game => Instance._game;
    public static InputManager Input => Instance._input;
    public static DataManager Data => Instance._data;
    public static SoundManager Sound => Instance._sound;
    public static UIManager UI => Instance._ui;
    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEX Scene => Instance._scene;
    #endregion

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Scene.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.T))
        {
            Game.SpawnBoss();
        }
    }

    // 각 매니저 초기화
    public void Init()
    {
        UI.Init();
        Sound.Init();
        Data.Init();
        Game.Init();
        //Input.Init();     // 인게임 시작 시, 초기화
    }

    public void OnDestroy()
    {
        Scene.Clear();
        //Input.Clear();    // 인게임 종료 시, 클리어
    }
}