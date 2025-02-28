using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    private CarController _car;

    private void OnTriggerEnter(Collider other)
    {
        _car.speed = _car.speed * 2;
        Destroy(gameObject);
    }
}
