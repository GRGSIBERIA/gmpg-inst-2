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
    /// プレイヤーのキャッシュ
    /// </summary>
    Transform player;

    /// <summary>
    /// プレイヤーの過去の時間
    /// </summary>
    Vector3 prevPosition;

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
        player = GameObject.FindWithTag("Player").transform;

        // 初期値として現在のプレイヤーの位置を代入しておく
        prevPosition = player.position;

        // Transformをキャッシュする
        ts = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームセット時にはPlayerが破棄されるので動くのを止める
        if (player == null) return;

        // プレイヤーの位置を時間で微分して速度を得る
        Vector3 velocity = (player.position - prevPosition) * Time.deltaTime;

        // さらに速度を微分して加速度を得る
        Vector3 accel = (velocity - prevVelocity) * Time.deltaTime;
        
        // カメラの動きは加速度でコントロールする
        Vector3 cameraPosition = accel * power;
        cameraPosition.z = ts.localPosition.z;  // Zの位置だけは固定

        // あえてカメラをプレイヤー速度に代入することで，カメラの振りを実現する
        // localPositionはローカル座標系の位置
        ts.localPosition = cameraPosition;

        // 現在と過去を置き換える
        prevPosition = player.position;
        prevVelocity = velocity;
    }
}
