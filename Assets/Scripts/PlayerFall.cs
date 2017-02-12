using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour {

    int playerLayer;

	void Start () {
        playerLayer = LayerMask.NameToLayer("Players");
	}
	
	void Update () {
       
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Ahhh");
        if (collider.gameObject.layer == playerLayer) {
            Debug.Log("Fall");
            collider.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(AnimatePlayerFall(collider.gameObject));
        }
    }

    private IEnumerator AnimatePlayerFall(GameObject player) {
        player.GetComponent<CharacterMovement>().StartIgnoringInput();
        Vector3 originalScale = player.transform.localScale;
        float totalTime = 0;
        while (totalTime < 3) {
            yield return new WaitForEndOfFrame();
            player.transform.localScale = 0.9f * player.transform.localScale;
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position, 0.5f);
            totalTime += Time.deltaTime;
        }

        player.transform.localScale = originalScale;
        player.GetComponent<CharacterMovement>().StopIgnoringInput();
        player.GetComponent<CharacterSpawn>().Spawn();
    }
}
