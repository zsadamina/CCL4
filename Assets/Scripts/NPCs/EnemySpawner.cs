using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float _enemySpawnStart;

    
     [SerializeField] private float _enemySpawnInterval;

    
     [SerializeField] private GameObject _enemy;
    
    private RoomGenerator _roomGenerator;
    
    void Awake()
    {
        _roomGenerator =  RoomGenerator.Instance;
    }
    void Start()
    {
        InvokeRepeating("SpawnEnemy", _enemySpawnStart * 60, _enemySpawnInterval * 60);
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawner Invoked");
       var existingEnemy =  GameObject.FindGameObjectWithTag(_enemy.tag);
       if (existingEnemy)
       {
           return;
       }
       var spawnObject =  _roomGenerator.paths.OrderBy( x => Random.value).First();
       var enemy = Instantiate(_enemy, spawnObject.transform.position, Quaternion.identity);
    }
    
}
