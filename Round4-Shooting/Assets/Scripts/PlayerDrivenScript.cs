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
    /// プレイヤーを移動させるための手続き
    /// </summary>
    void MovePlayer()
    {
        Vector3 direction = Vector3.zero;   // 移動方向

        if (Input.GetKey(KeyCode.UpArrow)) 
        {   // 上ボタン
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow)) 
        {   // 下ボタン
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {   // 右ボタン
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {   // 左ボタン
            direction += Vector3.left;
        }
        direction.Normalize();  // 移動方向を正規化する，長さ1のベクトル

        // 移動方向に対して移動量を掛けるだけ
        transform.position += direction * speed * Time.deltaTime;
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
        // GetKeyDownは押された瞬間を検知する，連打が必要
        if (Input.GetKey(KeyCode.Space)) 
        {   // スペースを押したら弾が出る
            var bullet = Instantiate(bulletObject);  // 登録されている弾を生成する
        }
    }

    // Update is called once per frame
    void Update()
    {
        AtomicMovePlayer();   // プレイヤーを移動
        ShootBullet();        // 弾を発射する
    }
}
