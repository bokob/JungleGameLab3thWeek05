using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using Random = UnityEngine.Random;

public class GameManager
{
    [Header("소환")]
    BoxCollider2D _areaCollider;
    List<Transform> _enemyList = new List<Transform>();
    public List<Transform> EnemyList => _enemyList;

    float _minDistance = 2f;
    int _spawnNormalEnemyCount = 16;    //  소환할 일반 적 수

    public void Init()
    {
        _areaCollider = GameObject.FindAnyObjectByType<Boundary>().GetComponent<BoxCollider2D>();
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int normalEnemyLength = Enum.GetValues(typeof(NormalMonsterType)).Length;
        for (int i = 0; i < _spawnNormalEnemyCount; i++)
        {
            int randomValue = Random.Range(0, normalEnemyLength);

            NormalMonsterType normalEnemyType = Util.IntToEnum<NormalMonsterType>(randomValue);
            string enemyName = normalEnemyType.ToString();

            Vector2 spawnPosition;
            int attempts = 0;
            do
            {
                float x = Random.Range(-_areaCollider.size.x / 2, _areaCollider.size.x / 2);
                float y = Random.Range(-_areaCollider.size.y / 2, _areaCollider.size.y / 2);

                spawnPosition = new Vector2(x, y);
                attempts++;
            } while (Physics2D.OverlapCircle(spawnPosition, _minDistance) && attempts < 100);


            GameObject enemy = Manager.Resource.Instantiate(enemyName);
            enemy.transform.position = spawnPosition;

            if (enemy != null)
            {
                _enemyList.Add(enemy.transform);
                Debug.Log("적 생성");
            }
        }
    }

    public void Clear()
    {
    }
}