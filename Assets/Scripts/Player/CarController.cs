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
    private float _maxSpeed, _maxSpeedTurbo = 10, _accelerationFactor, _decelerationFactor, _rotationSpeed, _rotationFactor, _boostLenght=0.75f;
    private bool _isAccelerating, _isTurbo;

    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private float _terrainSpeedVar;

    [SerializeField]
    private AnimationCurve _accelerationCurve;

    [SerializeField]
    public Transform frontSpawner;
    public Transform backSpawner;

    [SerializeField]
    private KeyCode _left, _right, _up;

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
        yield return new WaitForSeconds(_boostLenght); //on attend un temps donn� avant de mettre fin au trubo
        _isTurbo = false; //on passe le bool _isTurbo � false afin de mettre fin au turbo
    }

    void Update()
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~FORWARD MOVEMENT~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKeyDown(_up)) //si la touche est maintenue
        {
            _isAccelerating = true; //on passe _isAccelerating � true
        }
        if (Input.GetKeyUp(_up)) //si la touche est l�ch�e
        {
            _isAccelerating = false; //on passe _isAccelerating � false
        }

        if (Physics.Raycast(transform.position, transform.up * -1, out var info, 1, _layerMask)) //on cr�� un raycast vers le sol pour d�tecter le type de terrain surl lequel on est selon la layer du terrain
        {
            SlowTerrain terrainBelow = info.transform.GetComponent<SlowTerrain>(); //on rempli la variable info avec les informations de SlowTerrain r�cup�r�es par le raycast
            if (terrainBelow != null)//si le terrain en dessous n'est pas rien
            {
                _terrainSpeedVar = terrainBelow.speedVar; //la variable de variation de terrain prend la valeur qui correspond au terrain sur lequel se trouve le joueur
            }
            else
            {
                _terrainSpeedVar = 1; //sinon, le multiplicateur de vitesse de terrain reste � 1
            }
        }
        else
        {
            _terrainSpeedVar = 1; //s�curit� pour garder le multiplicateur de vitesse � 1 s'il n'y a pas de terrain
        }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURBO~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (_isTurbo) //si le joueur est en turbo
        {
            _maxSpeed = _maxSpeedTurbo; //on passe sa max speed � la vitesse max du turbo
            _speed = _accelerationCurve.Evaluate(_accelerationFactorLerpInterpolator) * _maxSpeed * _terrainSpeedVar;
        }
        else
        {
            _maxSpeed = 4;
            _speed = _accelerationCurve.Evaluate(_accelerationFactorLerpInterpolator) * _maxSpeed * _terrainSpeedVar;
        }

        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURNING~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKey(_left))
        {
            transform.eulerAngles -= Vector3.up * _rotationSpeed * Time.deltaTime * _rotationFactor;
        }
        if (Input.GetKey(_right))
        {
            transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime * _rotationFactor;
        }
    }
}