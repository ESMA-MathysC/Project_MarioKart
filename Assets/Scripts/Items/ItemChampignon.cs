using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "ItemChampignon", menuName = "Scriptable Objects/ItemChampignon")]
public class ItemChampignon : Item
{
    private GameObject playerKart;

    public override void Activation(PlayerItemManager player)
    {
        Debug.Log("Mushroom has been used");
    }
}
