using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBurstScript : MonoBehaviour
{
    IEnumerator DestroyMyself()
    {
        // 5秒後に自分自身を削除，５秒間炎上する
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 生成直後にコルーチンを呼び出し
        StartCoroutine("DestroyMyself");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
