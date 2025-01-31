using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //On créé une référnce à un rigidbody
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _speed, _maxSpeed, _accelerationFactor, _decelerationFactor, _rotationSpeed, _accelerationFactorLerpInterpolator;
    private bool _isAccelerating;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles -= Vector3.up *_rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += Vector3.up*_rotationSpeed * Time.deltaTime; ;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _isAccelerating = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _isAccelerating = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isAccelerating)
        {
            _accelerationFactorLerpInterpolator += _accelerationFactor;
            //_speed = Mathf.Clamp(_speed + _accelerationFactor * Time.fixedDeltaTime, 0, _maxSpeed );
        }
        else
        {
            _accelerationFactorLerpInterpolator -= _accelerationFactor*_decelerationFactor;

        }
        _accelerationFactorLerpInterpolator = Mathf.Clamp01(_accelerationFactorLerpInterpolator);

        _speed = _accelerationCurve.Evaluate(_accelerationFactorLerpInterpolator)*_maxSpeed;

        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);

    }
}