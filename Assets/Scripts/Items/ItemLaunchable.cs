using UnityEngine;

[CreateAssetMenu(fileName = "ItemLaunchable", menuName = "Scriptable Objects/ItemLaunchable")]
public class ItemLaunchable : Item
{
    public GameObject objectToSpawn;

    public override void Activation(PlayerItemManager player)
    {
        Instantiate(objectToSpawn, player.carController.backSpawner.transform.position, player.carController.backSpawner.transform.rotation); //instance le préfab de la peau de banane
    }
}
