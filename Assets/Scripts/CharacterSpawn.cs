using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour {
    public Transform spawnPoint;

    public void Spawn() {
        transform.position = spawnPoint.position;
        GetComponent<CharacterItemInteraction>().DestroyHolding();
    }
}
