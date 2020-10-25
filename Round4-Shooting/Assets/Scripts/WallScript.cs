using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {   // Bulletというタグがついている奴はデストロイ！
            Destroy(collider.gameObject);
        }
    }
}
