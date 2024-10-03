using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _playerBallTransform;
    private Rigidbody _rigidbody;

    private int _dificulty;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(_playerBallTransform.position.x, this.transform.position.y, this.transform.position.z), 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == _playerBallTransform.gameObject)
        {
            SceneManager.LoadScene(0);
        }
    }
}
