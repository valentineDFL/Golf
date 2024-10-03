using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

namespace Assets.Scripts.GameOver
{
    internal class StateLoader : MonoBehaviour
    {
        [SerializeField] private List<Coin> _coins = new List<Coin>();

        [SerializeField] private Ball _ball;
        [SerializeField] private Transform _arrowTransform;
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private MoveMarker _markerMover;

        private List<Vector3> _coinsPos = new List<Vector3>();
        private Vector3 _ballPos;

        private Vector3 _arrowRotate;
        private Vector3 _markerPos;

        private Vector3 _enemyPos;

        private void Awake()
        {
            for(int i = 0; i < _coins.Count; i++)
            {
                _coinsPos.Add(_coins[i].transform.position);
            }

            _ballPos = _ball.transform.position;
            _arrowRotate = _arrowTransform.rotation.eulerAngles;
            _markerPos = _markerMover.Marker.transform.position;
            _enemyPos = _enemyTransform.position;
        }

        private void OnEnable()
        {
            for(int i = 0; i < _coins.Count; i++)
            {
                _coins[i].OnCointCollected += ArrangeItems;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _coins.Count; i++)
            {
                _coins[i].OnCointCollected -= ArrangeItems;
            }
        }

        private void ArrangeItems()
        {

            LoadPositions();

            LoadRotations();

            EnableMarker();
        }

        private void LoadPositions()
        {
            Rigidbody ballRigidBody = _ball.GetComponent<Rigidbody>();

            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.transform.position = _ballPos;
            ballRigidBody.angularVelocity = Vector3.zero;

            _ball.GameStarted = false;

            _enemyTransform.position = _enemyPos;
        }

        private void LoadRotations()
        {
            _arrowTransform.gameObject.SetActive(true);
            _arrowTransform.rotation = Quaternion.Euler(_arrowRotate);
        }

        private void EnableMarker()
        {
            _markerMover.enabled = true;
            _markerMover.Marker.SetActive(true);
            
            _markerMover.Marker.transform.position = _markerPos;
        }
    }
}
