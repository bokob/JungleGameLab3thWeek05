using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;
using Random = UnityEngine.Random;

public class GameManager
{
    CameraController _cameraController;

    [Header("라운드")]
    public int CurrentRound => _currentRound;
    int _currentRound = 0;                                    // 현재 라운드

    [Header("소환")]
    public List<Transform> SpawnedList => _spawnedList;
    public List<Transform> NormalEnemyList => _normalEnemyList;
    PolygonCollider2D _polygonCollider;                         // 경기장 영역
    List<Transform> _spawnedList = new List<Transform>();       // 소환된 객체 리스트 (플레이어, 적) -> 개인전이므로 가장 가까운 적 탐색용 
    List<Transform> _normalEnemyList = new List<Transform>();   // 일반 몬스터 리스트
    float _minDistance = 2f;                                    // 일반 몬스터 소환될 때 간격
    int _spawnNormalEnemyCount = 15;                            // 소환할 일반 적 수

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
        NextRound();
    }

    // 플레이어 소환
    public void SpwanPlayer()
    {
        GameObject player = Manager.Resource.Instantiate("Player");
        player.transform.position = new Vector2(0, 0);
        Debug.Log("플레이어 생성");
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
    public void SpawnBoss()
    {
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
                enemy.transform.position = spawnPosition;
                if (enemy != null)
                {
                    _spawnedList.Add(enemy.transform);
                    _normalEnemyList.Add(enemy.transform);
                    Debug.Log("보스 생성");
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
        SpawnEnemy();
        Debug.Log($"다음 라운드 {_currentRound}");
    }

    public void Clear()
    {
    }
}