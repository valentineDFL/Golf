using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Marker
{
    internal class ArrowSizeScaler
    {
        private Vector3 _defaultScale;

        private ArrowRotater _arrow;
        private Transform _transform;

        public ArrowSizeScaler(ArrowRotater arrow, Transform markerPos)
        {
            _arrow = arrow;
            _transform = markerPos;

            _defaultScale = _arrow.transform.localScale;

        }

        public void ScaleByDistance()
        {
            Vector3 defaultScale = _arrow.transform.localScale;

            float distance = GetDistance();
            float newScale = _defaultScale.z + (distance / 10);

            _arrow.transform.localScale = new Vector3(defaultScale.x, defaultScale.y, newScale);
        }

        private float GetDistance()
        {
            Vector2 transformPosition = new Vector2(_transform.position.x, _transform.position.z);
            Vector2 arrowPosition = new Vector2(_arrow.transform.position.x, _arrow.transform.position.z);

            float distance = Vector2.Distance(transformPosition, arrowPosition);

            return distance;
        }
    }
}
