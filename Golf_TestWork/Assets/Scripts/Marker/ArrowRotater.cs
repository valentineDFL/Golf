using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Marker
{
    internal class ArrowRotater : MonoBehaviour
    {
        [SerializeField] private Transform _markerPos;
        private ArrowSizeScaler _scaler;

        private void Awake()
        {
            _scaler = new ArrowSizeScaler(this, _markerPos);
        }

        private void FixedUpdate()
        {
            _scaler.ScaleByDistance();
            CalculateAngle();
        }

        public void CalculateAngle()
        {
            float angleInRad = Mathf.Atan2(_markerPos.localPosition.z, _markerPos.localPosition.x);

            float angleInDegree = angleInRad * Mathf.Rad2Deg;

            this.transform.localRotation = Quaternion.Euler(0, -(angleInDegree - 90), 0);
        }
    }
}
