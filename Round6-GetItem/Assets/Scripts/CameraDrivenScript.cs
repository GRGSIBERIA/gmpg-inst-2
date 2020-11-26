using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrivenScript : MonoBehaviour
{
    /// <summary>
    /// カメラの振りの倍率
    /// </summary>
    [SerializeField]
    float power = 1f;

    /// <summary>
    /// プレイヤーのRigidbodyのキャッシュ
    /// </summary>
    Rigidbody playerrb;

    /// <summary>
    /// 過去の速度
    /// </summary>
    Vector3 prevVelocity;

    /// <summary>
    /// 自分のTransformのキャッシュ
    /// </summary>
    Transform ts;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーのキャッシュを確保する
        playerrb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        // Transformをキャッシュする
        ts = transform;

        // 過去の速度の初期値は現在速度
        prevVelocity = playerrb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームセット時にはPlayerが破棄されるので動くのを止める
        if (playerrb == null) return;

        Vector3 velocity = playerrb.velocity * Time.deltaTime;
        velocity.z = -10f;
        
        ts.localPosition = velocity;
    }
}
