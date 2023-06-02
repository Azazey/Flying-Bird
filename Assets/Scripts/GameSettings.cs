using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Toggle[] _difficults;
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _button;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        QualitySettings.shadows = ShadowQuality.Disable;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        QualitySettings.antiAliasing = 0;
        QualitySettings.shadowCascades = 0;
        Application.targetFrameRate = 1000;
        _toggle.onValueChanged.AddListener(SetMusicEnabled);
        _slider.onValueChanged.AddListener(SetVolume);
        _button.onClick.AddListener(ResetHighScore);
        
        if (!PlayerPrefs.HasKey(GameData.LevelDifficult))
        {
            GameData.SetLevelDifficult(0);
        }
        
        _difficults[PlayerPrefs.GetInt(GameData.LevelDifficult)].isOn = true;
        foreach (var toggle in _difficults)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
        _toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(SoundManager.Music));
        _slider.value = PlayerPrefs.GetFloat(SoundManager.Volume);
    }
    
    private void ResetHighScore()
    {
        GameData.ResetHighScore();
        ScreenFader.SChanger.LoadScene(0);
    }
    
    private void SetMusicEnabled(bool value)
    {
        if (SoundManager.Manager.MenuMusic)
        {
            SoundManager.Manager.MenuMusic.enabled = value;
        }
        PlayerPrefs.SetInt(SoundManager.Music, Convert.ToInt32(value));
    }
    
    private void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(SoundManager.Volume, value);
    }

    private void OnToggleValueChanged(bool value)
    {
        for (int i = 0; i < _difficults.Length; i++)
        {
            if (_difficults[i].isOn)
            {
                GameData.SetLevelDifficult(i);
            }
        }
    }
    
    private void OnDestroy()
    {
        foreach (var toggle in _difficults)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
