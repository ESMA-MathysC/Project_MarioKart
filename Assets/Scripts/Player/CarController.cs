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
    private float _speed, _accelerationFactorLerpInterpolator;

    [SerializeField]
    private float _maxSpeed, _maxSpeedTurbo = 10, _accelerationFactor, _decelerationFactor, _rotationSpeed, _rotationFactor;
    private bool _isAccelerating, _isTurbo;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURBO METHOD AND COROUTINE~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Turbo() //on créé une méthode qui permet de démarrer la couroutine d'accélération turbo
    {
        if (!_isTurbo) //si le joueur n'est pas actuellement en turbo
        {
            StartCoroutine(Turboroutine()); //alors on démarre la couroutine d'accélération turbo
        }
    }

    private IEnumerator Turboroutine() //on créé la coroutine de turbo
    {
        _isTurbo = true; //on passe le bool _isTurbo à true car le joueur entre en turbo
        yield return new WaitForSeconds(3); //on attend un temps donné aavant de mettre fin au trubo
        _isTurbo = false; //on passe le bool _isTurbo à false afin de mettre fin au turbo
    }

    void Update()
    {


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~FORWARD MOVEMENT~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKeyDown(KeyCode.UpArrow)) //si la touche est maintenue
        {
            _isAccelerating = true; //on passe _isAccelerating à true
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) //si la touche est lâchée
        {
            _isAccelerating = false; //on passe _isAccelerating à false
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


        if (_isAccelerating) //si le joueur est actuellement en train d'accélérer
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