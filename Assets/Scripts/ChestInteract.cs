using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ChestInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _spawnItem;

    [SerializeField]
    private Transform _spawnLocation;

    private bool _canInteract = true;

    public void Interact()
    {
        if (_canInteract)
        {
            StartCoroutine(SpawnScoreBall());
        }
    }


    IEnumerator SpawnScoreBall()
    {
        _canInteract = false;
        for(int i = 0;i < 5; i++)
        {
            Instantiate(_spawnItem, _spawnLocation.transform.position, _spawnLocation.transform.rotation);
            yield return new WaitForSeconds(.5f);
        }
        Destroy(this);
    }
}
