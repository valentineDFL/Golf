using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform _terrain;
    [SerializeField] protected List<Coin> _coins = new List<Coin>();

    private Rigidbody _rigidBody;

    private Vector3 _lastVelocity;

    private int _collectedCoinCount;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private bool _gameStarted;
    private float _idleTime;

    public bool GameStarted
    {
        set 
        {
            if (value != _gameStarted)
            {
                _gameStarted = value;
                _idleTime = 0;
            }
        }
    }

    private void Update()
    {
        _lastVelocity = _rigidBody.velocity;

        _idleTime += Time.deltaTime;

        if (_gameStarted && _idleTime > 7)
            SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != _terrain.gameObject)
        {
            _gameStarted = true;

            Vector3 newVelocity = Vector3.Reflect(_lastVelocity, collision.contacts[0].normal);

            _rigidBody.velocity = newVelocity;
        }

        if (collision.gameObject.GetComponent<Coin>())
        {
            _collectedCoinCount++;

            if( _collectedCoinCount == _coins.Count)
                SceneManager.LoadScene(0);
        }

    }
}
