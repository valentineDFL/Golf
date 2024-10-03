using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Marker
{
    internal class BallForcer : MonoBehaviour
    {
        [SerializeField] private Ball _ball;
        private Rigidbody _ballRigidBody;

        private void Awake()
        {
            _ballRigidBody = _ball.GetComponent<Rigidbody>();
        }

        public void Force(Vector3 direction)
        {
            _ballRigidBody.velocity = direction;
        }
    }
}
