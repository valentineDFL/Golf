using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public event Action OnCointCollected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            gameObject.SetActive(false);
            OnCointCollected?.Invoke();
        }
    }
}
