using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    private GameObject background;
    ProgressValues progressval;
    public float clamp = 0.9f;
    SpriteRenderer bs;
    SpriteRenderer fs;

    // Use this for initialization
    void Start()
    {
        background = this.transform.parent.gameObject;
        progressval = background.GetComponent<ProgressValues>();
        bs = background.GetComponent<SpriteRenderer>();
        fs = background.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float prog = Mathf.Clamp( progressval.getProgress(),0, clamp);
        if (progressval.learp)
            prog = Mathf.Lerp((float)this.transform.localScale.x, prog, 0.1f);
        this.transform.localScale = new Vector3(prog, this.transform.localScale.y, this.transform.localScale.z);
        
        //this.transform.position = new Vector3(  bs.sprite.bounds.size.x  - fs.sprite.bounds.size.x, this.transform.position.y, this.transform.position.z);

    }
    
}
