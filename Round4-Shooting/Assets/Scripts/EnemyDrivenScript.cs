using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrivenScript : MonoBehaviour
{
    /// <summary>
    /// 死に声の本体
    /// </summary>
    [SerializeField]
    GameObject deathVoice;

    /// <summary>
    /// 死に際の炎
    /// </summary>
    [SerializeField]
    GameObject deathBurst;

    /// <summary>
    /// 前向きな移動速度
    /// </summary>
    [SerializeField]
    float verticalSpeed;

    /// <summary>
    /// 横向きな移動速度
    /// </summary>
    float horizontalSpeed;

    /// <summary>
    /// 移動する周期
    /// </summary>
    float frequency;

    // Start is called before the first frame update
    void Start()
    {
        // 横方向にはランダムで移動しているように見せかける
        frequency = Random.Range(0.1f, 4f);
        horizontalSpeed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動方向を定義する
        // 正弦波を使って敵は左右に移動する
        var sinArgument = 2f * Mathf.PI * frequency * Time.time;

        // 移動方向
        var direction = new Vector3(
            0f,   //X
            0f,                                         //Y
            -verticalSpeed * Time.deltaTime);           //Z

        // その向きに移動させる
        // deltaTimeがないのは既にX軸成分で時間を使ってしまっているため
        transform.position += direction;
    }

    void OnCollisionEnter(Collision collider)
    {
        // もし，衝突相手がBulletの識別符号を持っていたら？
        if (collider.gameObject.tag == "Bullet")
        {
            // NOTE: 敵自身に鳴らせようとすると冗談にならないほど面倒

            // デスボイスを生成する
            var rubbish = Instantiate(deathVoice);          // デスボイス再生用のGameObjectを生成する
            rubbish.transform.position = transform.position;// 現在値とデスボイス用オブジェクトの位置を合わせる

            // きたねえ火花を生成する
            var burst = Instantiate(deathBurst);            // 当たった時の火炎
            burst.transform.position = transform.position;  // 位置は合わせておく
            burst.transform.eulerAngles = new Vector3(0f, 180f, 0f);    // 向きは180度逆

            Destroy(collider.gameObject);   // 弾は先に消しておく
            Destroy(this.gameObject);       // 最後に自分自身を消す
        }
    }
}
