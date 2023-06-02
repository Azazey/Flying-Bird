using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private GameObject[] _objectsToActiveAndDeactive;
    [SerializeField] private TextMeshProUGUI _endScore;

    private int _menuSceneNumber = 0;
    private int _gameSceneNumber = 1;


    private void Awake()
    {
        foreach (var obj in _objectsToActiveAndDeactive)
        {
            obj.SetActive(false);
        }
    }
    

    private void Start()
    {
        _startScreen.SetActive(true);
        _gameOverScreen.SetActive(false);
        _playerRigidbody.isKinematic = true;
        _menuButton.onClick.AddListener(OnMenuButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        GameData.OnGameStart();
        _score.gameObject.SetActive(false);
    }
    
    private void OnMenuButtonClick()
    {
        ScreenFader.SChanger.LoadScene(_menuSceneNumber);
    }

    private void OnRestartButtonClick()
    {
        ScreenFader.SChanger.LoadScene(_gameSceneNumber);
    }

    public void StartGame()
    {
        _playerRigidbody.isKinematic = false;
        _startScreen.SetActive(false);
        _score.gameObject.SetActive(true);
        foreach (var obj in _objectsToActiveAndDeactive)
        {
            obj.SetActive(true);
        }
    }

    public void GameOver()
    {
        _endScore.text = $"Score:{PlayerPrefs.GetInt(GameData.Score)}";
        _playerRigidbody.isKinematic = true;
        _gameOverScreen.SetActive(true);
        _score.gameObject.SetActive(false);
        GameData.SetHighScore();
        foreach (var obj in _objectsToActiveAndDeactive)
        {
            if (obj.GetComponent<TubeSpawner>())
            {
                obj.GetComponent<TubeSpawner>().StopAllTube();
            }
            else
            {
                obj.GetComponent<Tube>().GameOver();
            }
        }
    }

    public void OnScoreAdded()
    {
        GameData.AddScore(1);
        _score.text = PlayerPrefs.GetInt(GameData.Score).ToString();
    }
}
