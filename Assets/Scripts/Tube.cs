using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    private float _speed;
    private bool _gameOver = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt(GameData.LevelDifficult) == 0)
        {
            _speed = 0.05f;
        }
        else if (PlayerPrefs.GetInt(GameData.LevelDifficult) == 1)
        {
            _speed = 0.065f;
        }
        else if (PlayerPrefs.GetInt(GameData.LevelDifficult) == 2)
        {
            _speed = 0.08f;
        }
    }

    private void FixedUpdate()
    {
        if (!_gameOver)
        {
            var position = transform.position;
            _rigidbody.MovePosition(new Vector3(position.x - _speed, position.y, position.z));
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
