using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _flyForce;
    [SerializeField] private float _maxDrag;
    [SerializeField] private float _minDrag;
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private AudioSource _flySound;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private AudioSource _addScore;

    private float flyCoolDown = 0;
    
    private void Update()
    {
        flyCoolDown += Time.unscaledDeltaTime;
    }

    private void FixedUpdate()
    {
        _rigidbody.drag = Mathf.Clamp(_rigidbody.drag - 0.1f, _minDrag, _maxDrag);
    }

    public void Fly()
    {
        if ( flyCoolDown > 0.3f)
        {
            _rigidbody.drag = _maxDrag;
            _rigidbody.AddForce(0, _flyForce, 0, ForceMode.VelocityChange);
            flyCoolDown = 0;
            _flySound.Play();
        }
    }
    
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Tube") || collider.gameObject.layer == LayerMask.NameToLayer("OnlyCollideWithPlayer"))
        {
            _gameLogic.GameOver();
            _fallSound.Play();
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("AddScore"))
        {
            _gameLogic.OnScoreAdded();
            _addScore.Play();
        }
        
    }
}
