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
    /// 加速度を用いる場合は初速度，50 m/sぐらいがちょうどいい
    /// </summary>
    [SerializeField]
    float velocity = 1f;

    /// <summary>
    /// 血しぶきの加速度 [m/s^2]
    /// 減衰なしの等加速度運動
    /// </summary>
    [SerializeField]
    float accel = 1f;

    /// <summary>
    /// 速度を使うフラグ
    /// </summary>
    [SerializeField]
    bool isMotion = true;

    /// <summary>
    /// Transformのキャッシュ変数
    /// </summary>
    Transform ts;

    /// <summary>
    /// LineRendererのキャッシュ変数
    /// </summary>
    LineRenderer line;

    /// <summary>
    /// 1フレームの時間を固定する，処理落ちは気にしない
    /// </summary>
    const float dt = 1f / 60f;

    //******************************************************************************
    // ここから先はプロパティと呼ばれる文法で，メンバ変数に対するアクセス手段を提供する
    // get と set が両方存在すると勝手に変数が作られることがある
    // 普段はメンバ変数にアクセスさせる手段を減らすのが良い
    // この方法は，初期化用の関数が膨大になりそうなケースで有効ではある
    // 初期化する元(BloodEmitterScript.InstantiateParticle)が存在するパターンで有効な書き方
    // BloodEmitterScriptでは，**ランダム**に変数を初期化しているので，
    // このスクリプト中に処理を任せる（委譲）させるべきではない
    public float Lifetime { set { lifetime = value; }}

    public float Velocity { set { velocity = value; }}

    public float Accel { set { accel = value; }}

    public bool IsMotion { set { isMotion = value; }}
    //******************************************************************************

    /// <summary>
    /// エミッタの姿勢を転記する
    /// </summary>
    /// <param name="emitter">エミッタ</param>
    public void SetTransform(Transform emitter)
    {
        ts.position = emitter.position;
        ts.rotation = emitter.rotation;
        ts.localScale = emitter.localScale;
    }

    /// <summary>
    /// 回転で前向き姿勢を設定する
    /// SetTransformを先に呼び出す必要あり
    /// </summary>
    /// <param name="angleX"></param>
    /// <param name="angleY"></param>
    public void SetForward(float angleX, float angleY)
    {
        Quaternion rotX = Quaternion.AngleAxis(angleX, ts.right);
        Quaternion rotY = Quaternion.AngleAxis(angleY, ts.up);
        ts.rotation *= rotX * rotY;
    }

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
        velocity += accel * dt;

        // 姿勢前向きの変位, 変位は速度と時間の積で求まる
        Vector3 forward = velocity * dt * ts.forward;

        // 世界下向きの変位，等速直線運動にする
        Vector3 down = g * (dt) * Vector3.down;

        // 過去の座標を保存しておく
        Vector3 prevPosition = ts.position;

        // 姿勢正面の動きと世界下向き重力のベクトルを合成して座標に足す
        ts.position += forward + down;

        // 現在の座標と過去の座標から向きを求めて正面にする
        ts.forward = (ts.position - prevPosition).normalized;
    }

    void JudgeSurvivalTime()
    {
        // 生存時間が長くなったら自滅する
        if (lifetime < 0f)
        {
            Destroy(this.gameObject);
        }
        lifetime -= dt;
    }

    void OnCollisionEnter(Collision other) 
    {
        // 床と衝突したら勝手に消す
        if (other.gameObject.name == "Plane")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 生存時間の判定
        JudgeSurvivalTime();

        // 血しぶきの始点
        var f = ts.position;
        line.SetPosition(0, f);

        // 血しぶきの運動
        if (isMotion)
        {
            BloodMotion();
        }
        else
        {
            BloodAcceleration();
        }

        // 血しぶきの終点
        line.SetPosition(1, ts.position);
    }
}
