using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BloodScript : MonoBehaviour
{
    /// <summary>
    /// 血しぶきの生存時間
    /// </summary>
    [SerializeField]
    float lifetime = 1.5f;

    /// <summary>
    /// 血しぶきの速度 [m/s]
    /// </summary>
    [SerializeField]
    float velocity = 1f;

    /// <summary>
    /// 血しぶきの加速度 [m/s^2]
    /// 減衰なし
    /// </summary>
    [SerializeField]
    float accel = 1f;

    /// <summary>
    /// 血しぶきの回転速度 [deg/s]
    /// </summary>
    [SerializeField]
    float angleVelocity = 36f;

    /// <summary>
    /// Transformのキャッシュ変数
    /// </summary>
    Transform ts;

    /// <summary>
    /// LineRendererのキャッシュ変数
    /// </summary>
    LineRenderer line;

    /// <summary>
    /// 生存時間のカウンタ
    /// </summary>
    float lifetimeCounter = 0f;

    /// <summary>
    /// 1フレームの時間を固定する，処理落ちは気にしない
    /// </summary>
    const float dt = 1f / 60f;

    // Start is called before the first frame update
    void Start()
    {
        // transformを呼び出すたびにGetComponent関数が呼び出されるため，
        // tsという変数にキャッシュする
        ts = transform;

        // lineも同じ理由でGetComponentを更新するたびに呼び出すとコストがかかる
        line = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 簡単な運動のみで考える関数
    /// </summary>
    void BloodMotion()
    {
        // 血しぶきを回転させる
        ts.RotateAround(ts.position, ts.right, angleVelocity);

        // 変位とは，速度に微小時間を掛けたもの
        float displacement = velocity * dt;

        // 位置を変位の向きを足し合わせることで更新する
        ts.position += displacement * ts.forward;
    }

    /// <summary>
    /// 加速度や重力加速度を加味した運動
    /// </summary>
    void BloodAcceleration()
    {
        // 重力加速度 [m/s^2]
        // 重力加速度によってあたかも回転したかのように見せかける
        const float g = 9.8f;

        // 加速度を速度に変換する
        velocity = accel * dt;

        // 変位は速度の時間増分値
        float displacement = velocity * dt;

        // 姿勢前向きの変位
        Vector3 forward = velocity * ts.forward;

        // 世界下向きの変位
        // 重力加速度は求めるのにコストがかかるため，
        // 不定積分を2回やって等速とみなす
        // v = at
        // x = vt = 1/2 a t^2
        Vector3 down = 0.5f * g * (dt * dt) * Vector3.down;

        // 姿勢正面の動きと世界下向き重力のベクトルを合成して座標に足す
        ts.position += forward + down;
    }

    void JudgeSurvivalTime()
    {
        // 生存時間が長くなったら自滅する
        if (lifetimeCounter < lifetime)
        {
            Destroy(this.gameObject);
        }
        lifetimeCounter += dt;
    }

    // Update is called once per frame
    void Update()
    {
        // 生存時間の判定
        JudgeSurvivalTime();

        // 血しぶきの始点
        line.SetPosition(0, ts.position);

        // 血しぶきの運動
        BloodMotion();

        // 血しぶきの終点
        line.SetPosition(1, ts.position);
    }
}
