using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Random = UnityEngine.Random;

public class GameManager
{
    [Header("카메라")]
    public CameraController CameraController => _cameraController;
    CameraController _cameraController;

    [Header("라운드")]
    public int CurrentRound => _currentRound;
    int _currentRound = 0;                    // 현재 라운드

    [Header("소환")]
    public List<Transform> SpawnedList => _spawnedList;
    public List<Transform> NormalEnemyList => _normalEnemyList;
    public bool IsExistBoss { get { return _isExistBoss; } set { _isExistBoss = value; } }
    PolygonCollider2D _polygonCollider;                         // 경기장 영역
    List<Transform> _spawnedList = new List<Transform>();       // 소환된 객체 리스트 (플레이어, 적) -> 개인전이므로 가장 가까운 적 탐색용 
    List<Transform> _normalEnemyList = new List<Transform>();   // 일반 몬스터 리스트
    float _minDistance = 2f;                                    // 일반 몬스터 소환될 때 간격
    int _spawnNormalEnemyCount = 15;                            // 소환할 일반 적 수
    bool _isExistBoss = false;                                  // 보스 존재 여부

    public void Init()
    {
        Debug.Log("GameManager 생성");
    }

    public void GameStart()
    {
        _polygonCollider = GameObject.FindAnyObjectByType<ArenaGround>().GetComponent<PolygonCollider2D>();
        _cameraController = Camera.main.GetComponent<CameraController>();
        SpwanPlayer();
        _cameraController.Init();
        _currentRound++;
        Manager.UI.toggleRoundStartCanvasAction?.Invoke();  // 라운드 시작 UI
        Debug.LogWarning("~게임 시작~");
    }

    // 플레이어 소환
    public void SpwanPlayer()
    {
        GameObject player = Manager.Resource.Instantiate("Player");
        player.transform.position = new Vector2(0, -5);
        _spawnedList.Add(player.transform);
    }

    // 적 소환
    public void SpawnEnemy()
    {
        int normalEnemyLength = Enum.GetValues(typeof(NormalMonsterType)).Length;
        Bounds bounds = _polygonCollider.bounds;
        for (int i = 0; i < _spawnNormalEnemyCount; i++)
        {
            int randomValue = Random.Range(0, normalEnemyLength);
            NormalMonsterType normalEnemyType = Util.IntToEnum<NormalMonsterType>(randomValue);
            string enemyName = normalEnemyType.ToString();

            int attempts = 0;
            Vector2 spawnPosition;
            do
            {
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float y = Random.Range(bounds.min.y, bounds.max.y);
                spawnPosition = new Vector2(x, y);

                if(_polygonCollider.OverlapPoint(spawnPosition))
                {
                    GameObject enemy = Manager.Resource.Instantiate(enemyName);
                    enemy.GetComponent<Status>().DieAction += TrySpawnBoss;
                    enemy.transform.position = spawnPosition;
                    if (enemy != null)
                    {
                        _spawnedList.Add(enemy.transform);
                        _normalEnemyList.Add(enemy.transform);
                        Debug.Log("적 생성");
                    }
                    break;
                }
                attempts++;
            } while (Physics2D.OverlapCircle(spawnPosition, _minDistance) && attempts < 100);
        }
    }

    // 보스 소환
    public void TrySpawnBoss()
    {
        // 일반 몬스터 다 죽었는지 검사
        int deadEnemy = 0;
        foreach (Transform normalEnemy in _normalEnemyList)
        {
            if (normalEnemy.GetComponent<Status>().IsDead)
                deadEnemy++;
        }

        // 종료 조건
        if (deadEnemy < _spawnNormalEnemyCount)
        {
            //Debug.LogError("보스 소환 조건 미충족");
            return;
        }
        
        // 일반 몬스터 정리
        foreach (Transform normalEnemy in _normalEnemyList)
        {
            if (normalEnemy != null)
                GameObject.Destroy(normalEnemy.gameObject);
        }

        // 보스 소환
        _isExistBoss = true;
        BossMonsterType bossEnemyType = Util.IntToEnum<BossMonsterType>(_currentRound - 1);
        string enemyName = bossEnemyType.ToString();
        Bounds bounds = _polygonCollider.bounds;

        int attempts = 0;
        Vector2 spawnPosition;
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            spawnPosition = new Vector2(x, y);

            if (_polygonCollider.OverlapPoint(spawnPosition))
            {
                GameObject enemy = Manager.Resource.Instantiate(enemyName);

                if (_currentRound == 1)
                    enemy.GetComponent<Status>().DieAction += NextRound;
                
                enemy.transform.position = spawnPosition;
                if (enemy != null)
                {
                    _spawnedList.Add(enemy.transform);
                }
                break;
            }
            attempts++;
        } while (Physics2D.OverlapCircle(spawnPosition, _minDistance) && attempts < 100);
    }

    // 다음 라운드
    public void NextRound()
    {
        _currentRound++;
        Manager.UI.toggleRoundStartCanvasAction?.Invoke();  // 라운드 시작 UI
        Debug.Log($"다음 라운드 {_currentRound}");
        SpawnHiddenBoss();
    }

    /* 히든 보스 곧 추가될거 생각하고 작성한거라 오류가 날겁니다. */
    // 히든 보스 소환
    public void SpawnHiddenBoss()
    {
        string hiddenBossName = Define.BossMonsterType.Yasuo.ToString();
        GameObject hiddenBoss = Manager.Resource.Instantiate(hiddenBossName);
        hiddenBoss.GetComponent<Status>().DieAction += Manager.UI.toggleGameClearCanvasAction;
    }

    public void Clear()
    {
        _isExistBoss = false;
        _spawnedList.Clear();
        _normalEnemyList.Clear();
        _currentRound = 0;
    }
}