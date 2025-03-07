using UnityEngine;

public class Item : ScriptableObject //on créé un nouveau type de scriptable object : les items
{
    //on donne aux items plusieurs paramètres partagés entre les différents items : sprite, nom, nombre d'utilisations
    public Sprite itemSprite;
    public string itemName;
    public int itemUseCount = 1;

    public virtual void Activation(PlayerItemManager player)
    {

    }
}
