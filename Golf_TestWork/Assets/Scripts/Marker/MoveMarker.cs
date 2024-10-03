using System;
using Assets.Scripts.Marker;
using UnityEngine;

public class MoveMarker : MonoBehaviour
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private GameObject _arrowNose;
    private Rigidbody _markerRigidbody;

    private Vector3 _targetPos;

    private bool _isMoving;

    [SerializeField] private Transform _terrain;
    [SerializeField] private ArrowRotater _arrowRotater;

    private BallForcer _ballForcer;

    [SerializeField][Range(0, 50)] private float _powerMultiplyCoff;
    private float _power;

    public GameObject Marker => _marker;

    private void Awake()
    {
        _ballForcer = GetComponent<BallForcer>();
        _markerRigidbody = _marker.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Transform nose = _arrowNose.transform;
        Transform noseGrandpa = nose.transform.parent.parent;

        if (Input.GetMouseButtonDown(0))
        {
            _isMoving = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _isMoving = false;

            Vector3 direction = noseGrandpa.InverseTransformPoint(nose.position).normalized;
            Vector3 power = direction * _power;

            _ballForcer.Force(new Vector3(power.x, 0, power.z));

            _marker.gameObject.SetActive(false);
            _arrowNose.transform.parent.gameObject.SetActive(false);

            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 position = new Vector3(hit.point.x, 0, hit.point.z);
                _targetPos = position;
            }

            Vector2 marketPosition = new Vector2(_marker.transform.position.x, _marker.transform.position.z);
            Vector2 arrowRotaterPosition = new Vector2(_arrowRotater.transform.position.x, _arrowRotater.transform.position.z);

            float distance = Mathf.Clamp(Vector2.Distance(marketPosition, arrowRotaterPosition), 0, 5);

            _power = distance * _powerMultiplyCoff;

            _markerRigidbody.transform.position = Vector3.Lerp(_marker.transform.position, new Vector3(_targetPos.x, _marker.transform.position.y, _targetPos.z), 0.5f);
        }
    }
}
