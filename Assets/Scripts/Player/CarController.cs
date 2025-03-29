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
        yield return new WaitForSeconds(_boostLenght); //on attend un temps donné avant de mettre fin au trubo
        _isTurbo = false; //on passe le bool _isTurbo à false afin de mettre fin au turbo
    }

    void Update()
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~FORWARD MOVEMENT~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (Input.GetKeyDown(_up)) //si la touche est maintenue
        {
            _isAccelerating = true; //on passe _isAccelerating à true
        }
        if (Input.GetKeyUp(_up)) //si la touche est lâchée
        {
            _isAccelerating = false; //on passe _isAccelerating à false
        }

        if (Physics.Raycast(transform.position, transform.up * -1, out var info, 1, _layerMask)) //on créé un raycast vers le sol pour détecter le type de terrain surl lequel on est selon la layer du terrain
        {
            SlowTerrain terrainBelow = info.transform.GetComponent<SlowTerrain>(); //on rempli la variable info avec les informations de SlowTerrain récupérées par le raycast
            if (terrainBelow != null)//si le terrain en dessous n'est pas rien
            {
                _terrainSpeedVar = terrainBelow.speedVar; //la variable de variation de terrain prend la valeur qui correspond au terrain sur lequel se trouve le joueur
            }
            else
            {
                _terrainSpeedVar = 1; //sinon, le multiplicateur de vitesse de terrain reste à 1
            }
        }
        else
        {
            _terrainSpeedVar = 1; //sécurité pour garder le multiplicateur de vitesse à 1 s'il n'y a pas de terrain
        }
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~TURBO~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (_isTurbo) //si le joueur est en turbo
        {
            _maxSpeed = _maxSpeedTurbo; //on passe sa max speed à la vitesse max du turbo
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