using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrivenScript : MonoBehaviour
{
    /// <summary>
    /// キーが入力されたときのプレイヤーの移動速度
    /// </summary>
    [SerializeField]
    float speed = 1.0f;

    /// <summary>
    /// 発射する弾
    /// </summary>
    [SerializeField]
    GameObject bulletObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 原始的な移動方法
    /// </summary>
    void AtomicMovePlayer()
    {
        float moveAcount = speed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0f, 0f, moveAcount);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0f, 0f, -moveAcount);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-moveAcount, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(moveAcount, 0f, 0f);
        }
    }

    /// <summary>
    /// プレイヤーが弾を発射するための手続き
    /// </summary>
    void ShootBullet()
    {
        // GetKeyは押し続けると反応する
        if (Input.GetKey(KeyCode.Space)) 
        {   // スペースを押したら弾が出る
            Instantiate(bulletObject);  // 登録されている弾を生成する
        }
    }

    // Update is called once per frame
    void Update()
    {
        AtomicMovePlayer();   // プレイヤーを移動
        ShootBullet();        // 弾を発射する
    }
}
