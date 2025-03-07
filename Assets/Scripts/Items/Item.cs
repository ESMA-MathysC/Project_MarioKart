using UnityEngine;

public class Item : ScriptableObject //on cr�� un nouveau type de scriptable object : les items
{
    //on donne aux items plusieurs param�tres partag�s entre les diff�rents items : sprite, nom, nombre d'utilisations
    public Sprite itemSprite;
    public string itemName;
    public int itemUseCount = 1;

    public virtual void Activation(PlayerItemManager player)
    {

    }
}
