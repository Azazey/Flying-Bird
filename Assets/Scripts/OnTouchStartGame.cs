using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnTouchStartGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameLogic _gameLogic;
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_gameLogic)
        {
            _gameLogic.StartGame();
        }
    }
}
