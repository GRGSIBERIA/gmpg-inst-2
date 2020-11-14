using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrivenScript : MonoBehaviour
{
    /// <summary>
    /// 左右の移動速度 [m/s]
    /// </summary>
    [SerializeField, Tooltip("移動速度 [m/s]")]
    float moveSpeed;

    [SerializeField, Tooltip("ジャンプしたときの上向き初速 [m/s]")]
    float jumpingInitialVelocity;

    /// <summary>
    /// 左右の移動加速度 [m/s^2]
    /// </summary>
    [SerializeField, Tooltip("移動加速度 [m/s^2]")]
    float moveAccel;

    /// <summary>
    /// 現在の速度 [m/s]
    /// </summary>
    Vector3 velocity;

    /// <summary>
    /// Transformのキャッシュ
    /// GetComponent<Transform>()を呼び出すのを防ぐ
    /// </summary>
    Transform ts;

    /// <summary>
    /// 足場に足が付いているか？
    /// </summary>
    bool isFooting = false;

    // Start is called before the first frame update
    void Start()
    {
        ts = transform;
    }

    /// <summary>
    /// 足場に常駐している間に呼び出される関数
    /// 戻り地の前にpublicを付けると他のスクリプトから呼び出せるようになる
    /// </summary>
    public void StayStep()
    {
        isFooting = true;
    }

    /// <summary>
    /// 足場から離れたときに呼び出す関数
    /// </summary>
    public void ExitStep()
    {
        isFooting = false;
    }

    void PlayerInput()
    {
        // 向きベクトル
        Vector3 direction = Vector3.zero;

        // プレイヤーのキー入力に応じて向きを変える
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.right;
        }
        direction.Normalize();   // 向きを正規化する，同時押し対策もする

        // 左右方向の加速度を足し合わせる
        velocity += direction * moveAccel * Time.deltaTime;
        
        // 重力加速度を速度に足す
        velocity += Vector3.down * 9.8f * Time.deltaTime;

        // 速度を変位に変換する
        ts.position += velocity * Time.deltaTime;

        /// NOTE: 次元解析 (解析学13回あたり？)
        /// 時速 30 km/h の車が 3 時間等速に移動した距離は？
        /// = 90 km 移動したことになる
        /// 10 m/s^2 のプレイヤーが1フレーム時間移動した距離は？
        /// = 10/60 m/s の速度で移動する，距離の問題に直すと
        /// = 10/60 * 10/60 m = 100/3600 m = 1/36 m 移動する
        /// 60 フレーム後には？ => 60/36 m 移動している
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
}
