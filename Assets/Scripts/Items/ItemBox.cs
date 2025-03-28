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

    private void OnTriggerEnter(Collider other) //méthode utilisée afin de détecter l'entrée du joueur dans le trigger de l'Item Box
    {
        PlayerItemManager playerItemManagerInContact = other.GetComponent<PlayerItemManager>(); //on créé une variable temporaire "playerItemManagerInContact" de type "PlayerItemManager", other va donner les informations de "PlayerItemManager" à "playerItemManagerInContact", ce dernier peut donc utiliser et accéder aux méthodes et variable de PlayerItemManager
        if (playerItemManagerInContact != null) //on vérifie que "playerItemManagerInContact" n'est pas vide
        {
            playerItemManagerInContact.GenerateItem(); //on déclenche la méthode "GenerateItem()" qui se trouve dans le script "PlayerItemManager"
            StartCoroutine(Respawn()); //déclenchement de la couroutine de respawn
        }
    }

    private IEnumerator Respawn() //coroutine qui gère la disparition, réapparition et temps de respawn de l'Item Box
    {
        _collider.enabled = false; //on désactive son collider
        _text.enabled = false; //le texte "?"
        _meshRenderer.enabled = false; //et le mesh
        yield return new WaitForSeconds(waitBeforeRespawn); //on attend un temps donné avant la réapparition de l'Item Box
        _collider.enabled = true; //puis on réactive son collider
        _text.enabled = true; //le texte "?"
        _meshRenderer.enabled = true; //et le mesh
    }
}
