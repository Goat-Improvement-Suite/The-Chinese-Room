using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour {
    public Transform spawnPoint;
    public AudioClip spawnSound;
    public void Spawn() {
        transform.position = spawnPoint.position;
        AudioSource.PlayClipAtPoint(spawnSound, spawnPoint.position);
        GetComponent<CharacterItemInteraction>().DestroyHolding();
    }
}
