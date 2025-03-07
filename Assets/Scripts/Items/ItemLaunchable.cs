using UnityEngine;

[CreateAssetMenu(fileName = "ItemLaunchable", menuName = "Scriptable Objects/ItemLaunchable")]
public class ItemLaunchable : Item
{
    public GameObject objectToLaunch;

    public override void Activation(PlayerItemManager player)
    {
        Debug.Log(objectToLaunch.name + " has been thrown");
        Instantiate(objectToLaunch); //instance le préfab de la carapace
    }
}
