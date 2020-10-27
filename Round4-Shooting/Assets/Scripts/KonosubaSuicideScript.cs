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
        // 自分の子供のパーティクルが自滅したら
        // 自分も自滅するような処理
        if (transform.childCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
