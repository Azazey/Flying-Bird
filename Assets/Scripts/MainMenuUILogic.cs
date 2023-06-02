using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUILogic : MonoBehaviour
{
    [SerializeField] private Animator _settingsAnimator;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _settingsExitButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private AppsFlyerObjectScript _appsFlyer;
    [SerializeField] private TextMeshProUGUI _conversionDataText;
    [SerializeField] private TextMeshProUGUI _highScore;

    private const int _gameSceneNumber = 1;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _settingsExitButton.onClick.AddListener(OnSettingsExitButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _appsFlyer.ConversionDataEvent += AppsFlyerOnConversionDataEvent;
        _highScore.text = $"High Score:{PlayerPrefs.GetInt(GameData.HighScore).ToString()}";
    }
    

    private void AppsFlyerOnConversionDataEvent(string obj)
    {
        _conversionDataText.text = obj;
    }

    private void OnStartButtonClick()
    {
        ScreenFader.SChanger.LoadScene(_gameSceneNumber);
    }

    private void OnSettingsButtonClick()
    {
        _settingsAnimator.SetTrigger("SlideIn");
    }

    private void OnSettingsExitButtonClick()
    {
        _settingsAnimator.SetTrigger("SlideOut");
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
    
}
