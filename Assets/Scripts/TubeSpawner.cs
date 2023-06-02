using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawner : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Tube _tubePrefab;
    
    private float _spawnTimer;
    private Pool<Tube> _pool;
    private bool _gameOver;

    public float SpawnTimer
    {
        get => _spawnTimer;
        set => _spawnTimer = value;
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt(GameData.LevelDifficult) == 0)
        {
            _spawnTimer = 3f;
        }
        else if (PlayerPrefs.GetInt(GameData.LevelDifficult) == 1)
        {
            _spawnTimer = 2f;
        }
        else if (PlayerPrefs.GetInt(GameData.LevelDifficult) == 2)
        {
            _spawnTimer = 1.5f;
        }
        _pool = new Pool<Tube>(_tubePrefab, _poolCount, _autoExpand, transform);
        Invoke("SpawnFood", _spawnTimer);
    }

    public void StopAllTube()
    {
        _gameOver = true;
        foreach (Tube tube in _pool.PoolList)
        {
            tube.GameOver();
        }
    }

    public void SpawnFood()
    {
        if (!_gameOver)
        {
            var tube = _pool.GetFreeElement();
            tube.transform.position = new Vector3(0f,Random.Range(-12f,-3.5f),0f);
            Invoke("SpawnFood", _spawnTimer);
        }
    }
}
