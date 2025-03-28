using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _itemList; //cr�� la liste d'items que le joueur pourra obtenir

    [SerializeField]
    private Item _currentItem; //permet de savoir quel est l'item actuel

    [SerializeField]
    private Image _itemImage; //permet de changer le sprite de l'UI pour celui de l'item actuel

    [SerializeField]
    private int _numberOfItemUse; //permet de savoir combien d'utilisations il reste � l'item actuel

    [SerializeField]
    private KeyCode _useObject;

    public CarController carController;

    private void Update()
    {
        if (Input.GetKeyDown(_useObject)) //input utilis� afin d'activer l'objet actuel
        {
            UseItem(); //d�clenchement de la m�thode d'activation d'items
        }
    }
    public void GenerateItem() //m�thode qui g�n�re l'objet obtenu lorsque le joueur trigger une Item Box
    {
        if(_currentItem == null) //si le joueur n'a actuellement pas d'item (currentItem == null signifie qu'il n'y a rien)
        {
            _currentItem = _itemList[Random.Range(0, _itemList.Count)]; //on attribue � currentItem un item al�atoire venant de la liste d'item obtenables, le Random.Range va de 0 � la taille de la liste
            _itemImage.sprite = _currentItem.itemSprite; //on change l'image de l'item qui est affich�e sur l'UI
            _numberOfItemUse = _currentItem.itemUseCount; //on attirbue le nombre d'utilisation de l'item obtenu selon le nombre d'utilisation donne par le Scriptable Object
        }

    }

    public void UseItem() //m�thode parmettant d'activer un item
    {
        if (_currentItem != null) //utilisable seulement si l'item actuel n'est pas null
        {
            _currentItem.Activation(this); //
            _numberOfItemUse--; //on soustrait 1 au nombre d'utilisation restante  l'item (n�cessaire pour les items � utilisation multiple mais aussi pour les singulier, savoir quand ils sont �puis�s)
            if(_numberOfItemUse <=0) //si l'item a un nombre d'utilisation de 0 ou moins alors on pass _currentItem et le sprite de l'UI � null
            {
                _currentItem = null;
                _itemImage.sprite = null;
            }
        }
    }


}
