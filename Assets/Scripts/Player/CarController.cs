using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //On cr�� une r�f�rnce � un rigidbody
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _speed, _accelerationFactorLerpInterpolator;

    [SerializeField]
    private float _maxSpeed, _maxSpeedTurbo = 10, _accelerationFactor, _decelerationFactor, _rotationSpeed, _rotationFactor;
    private bool _isAccelerating, _isTurbo;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURBO METHOD AND COROUTINE~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Turbo() //on cr�� une m�thode qui permet de d�marrer la couroutine d'acc�l�ration turbo
    {
        if (!_isTurbo) //si le joueur n'est pas actuellement en turbo
        {
            StartCoroutine(Turboroutine()); //alors on d�marre la couroutine d'acc�l�ration turbo
        }
    }

    private IEnumerator Turboroutine() //on cr�� la coroutine de turbo
    {
        _isTurbo = true; //on passe le bool _isTurbo � true car le joueur entre en turbo
        yield return new WaitForSeconds(3); //on attend un temps donn� aavant de mettre fin au trubo
        _isTurbo = false; //on passe le bool _isTurbo � false afin de mettre fin au turbo
    }

    void Update()
    {


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~FORWARD MOVEMENT~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKeyDown(KeyCode.UpArrow)) //si la touche est maintenue
        {
            _isAccelerating = true; //on passe _isAccelerating � true
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) //si la touche est l�ch�e
        {
            _isAccelerating = false; //on passe _isAccelerating � false
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
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~BASIC ACCELERATION~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        if (_isAccelerating) //si le joueur est actuellement en train d'acc�l�rer
        {
            _accelerationFactorLerpInterpolator += _accelerationFactor;
        }
        else
        {
            _accelerationFactorLerpInterpolator -= _accelerationFactor*_decelerationFactor;
        }

        _accelerationFactorLerpInterpolator = Mathf.Clamp01(_accelerationFactorLerpInterpolator);

        _speed = _accelerationCurve.Evaluate(_accelerationFactorLerpInterpolator)*_maxSpeed;

        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURNING~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles -= Vector3.up * _rotationSpeed * Time.deltaTime * _rotationFactor;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime * _rotationFactor;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURBO~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (_isTurbo) //si le joueur est en turbo
        {
            _speed = _maxSpeedTurbo;
        }
        else
        {
            _speed = _accelerationCurve.Evaluate(_accelerationFactorLerpInterpolator) * _maxSpeed;
        }

    }
}