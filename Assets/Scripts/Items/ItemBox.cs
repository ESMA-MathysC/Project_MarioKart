using System.Collections;
using TMPro;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer, _text;
    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private float waitBeforeRespawn = 1;

    private void OnTriggerEnter(Collider other) //m�thode utilis�e afin de d�tecter l'entr�e du joueur dans le trigger de l'Item Box
    {
        PlayerItemManager playerItemManagerInContact = other.GetComponent<PlayerItemManager>(); //on cr�� une variable temporaire "playerItemManagerInContact" de type "PlayerItemManager", other va donner les informations de "PlayerItemManager" � "playerItemManagerInContact", ce dernier peut donc utiliser et acc�der aux m�thodes et variable de PlayerItemManager
        if (playerItemManagerInContact != null) //on v�rifie que "playerItemManagerInContact" n'est pas vide
        {
            playerItemManagerInContact.GenerateItem(); //on d�clenche la m�thode "GenerateItem()" qui se trouve dans le script "PlayerItemManager"
            StartCoroutine(Respawn()); //d�clenchement de la couroutine de respawn
        }
    }

    private IEnumerator Respawn() //coroutine qui g�re la disparition, r�apparition et temps de respawn de l'Item Box
    {
        _collider.enabled = false; //on d�sactive son collider
        _text.enabled = false; //le texte "?"
        _meshRenderer.enabled = false; //et le mesh
        yield return new WaitForSeconds(waitBeforeRespawn); //on attend un temps donn� avant la r�apparition de l'Item Box
        _collider.enabled = true; //puis on r�active son collider
        _text.enabled = true; //le texte "?"
        _meshRenderer.enabled = true; //et le mesh
    }
}
