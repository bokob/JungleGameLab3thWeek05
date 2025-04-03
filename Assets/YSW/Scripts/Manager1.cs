using UnityEngine;
using System.Collections.Generic;

public class Manager1 : MonoBehaviour
{
    static Manager1 _instance;
    public static Manager1 Instance => _instance;

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

    // 적 트랜스폼 리스트 추가
    private List<Transform> _enemyTransforms = new List<Transform>();

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

    public void Init()
    {
        Game.Init();
        Input.Init();
        Data.Init();
        Sound.Init();
        UI.Init();
        // 적 리스트 초기화 (필요하면 여기서 적 추가)
    }

    public void OnDestroy()
    {
        Input.Clear();
    }

    // 적 추가 메서드
    public void AddEnemy(Transform enemyTransform)
    {
        if (!_enemyTransforms.Contains(enemyTransform))
        {
            _enemyTransforms.Add(enemyTransform);
        }
    }

    // 적 제거 메서드
    public void RemoveEnemy(Transform enemyTransform)
    {
        _enemyTransforms.Remove(enemyTransform);
    }

    // 적 리스트 반환
    public List<Transform> GetEnemyTransforms()
    {
        return _enemyTransforms;
    }
}