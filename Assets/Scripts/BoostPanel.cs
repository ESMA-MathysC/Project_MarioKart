using UnityEngine;

public class BoostPanel : MonoBehaviour
{
    private void OnTriggerEnter(Collider player)
    {
        if (CompareTag("BoostPanel"))
        {
            player.GetComponent<CarController>().Turbo();
        }
    }
}
