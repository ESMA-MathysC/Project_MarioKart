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
    private float _speed, _maxSpeed, _backwardsSpeed, _maxBackwardsSpeed, _accelerationFactor, _accelerationFactorLerpInterpolator, _decelerationFactor, _rotationSpeed, _rotationFactor;
    private bool _isAccelerating, _isGoingBackwards;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    void Start()
    {
        
    }

    void Update()
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURNING~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles -= Vector3.up *_rotationSpeed * Time.deltaTime * _rotationFactor;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += Vector3.up*_rotationSpeed * Time.deltaTime * _rotationFactor;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~FORWARD MOVEMENT~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _isAccelerating = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _isAccelerating = false;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~BACKWARDS MOVEMENT~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /*if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _isGoingBackwards = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _isGoingBackwards = false;
        }*/
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

        /*if (_isGoingBackwards)
        {
         
        }*/

        _accelerationFactorLerpInterpolator = Mathf.Clamp01(_accelerationFactorLerpInterpolator);

        _speed = _accelerationCurve.Evaluate(_accelerationFactorLerpInterpolator)*_maxSpeed;

        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);

    }
}