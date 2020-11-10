using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonosubaSuicideScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 自分の子供のすべてのパーティクルが自滅したら
        // 自分自身も自滅させる
        if (transform.childCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
