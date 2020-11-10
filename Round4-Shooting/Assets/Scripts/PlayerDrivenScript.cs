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
        Vector3 displacement = Vector3.zero;
        
        // 変位をキー入力で導く
        if (Input.GetKey(KeyCode.UpArrow))
        {
            displacement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            displacement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            displacement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            displacement += Vector3.right;
        }
        displacement.Normalize();   // 変位ベクトルを正規化する

        // 速度に微小時間を掛けて移動距離＝変位を求める
        // 変位ベクトルに掛けることで移動量が一定になる
        transform.position += displacement * speed * Time.deltaTime;
    }

    /// <summary>
    /// プレイヤーが弾を発射するための手続き
    /// </summary>
    void ShootBullet()
    {
        // GetKeyは押し続けると反応する
        if (Input.GetKeyDown(KeyCode.Space)) 
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
