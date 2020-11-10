using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEmitterScript : MonoBehaviour
{
    /// <summary>
    /// パーティクルの動作の種類
    /// </summary>
    public enum ParticleDriveType
    {
        Motion,
        Accelaration
    }

    /// <summary>
    /// パーティクルのPrefab
    /// </summary>
    [SerializeField]
    GameObject particle;

    /// <summary>
    /// 動作方法
    /// </summary>
    [SerializeField]
    ParticleDriveType driveType;

    /// <summary>
    /// 生存時間の幅
    /// </summary>
    [SerializeField, Header("パーティクルの設定(x:最小, y:最大)")]
    Vector2 lifetime = new Vector2(3f, 5f);

    /// <summary>
    /// 速度の幅
    /// </summary>    
    [SerializeField]
    Vector2 velocity = new Vector2(3f, 10f);

    /// <summary>
    /// 加速度の幅
    /// </summary>
    [SerializeField]
    Vector2 accel = new Vector2(40f, 80f);

    /// <summary>
    /// 角速度の幅
    /// </summary>
    [SerializeField]
    Vector2 angularVelocity = new Vector2(0f, 20f);

    /// <summary>
    /// エミッタの生存時間
    /// </summary>
    [SerializeField, Header("エミッタの設定")]
    float emitterLifetime;

    /// <summary>
    /// エミッタの生成角度
    /// </summary>
    [SerializeField]
    float emitterAngle = 45f;

    /// <summary>
    /// 1秒間に600個のパーティクルを生成する
    /// </summary>
    [SerializeField]
    float particlePerSec = 600f;

    /// <summary>
    /// 1フレームでパーティクルを生成する個数
    /// </summary>
    int particlePerFrame;

    /// <summary>
    /// フレームあたりに発生するパーティクル数の揺らぎ
    /// </summary>
    [SerializeField]
    int particleFluctuationPerFrame = 4;

    /// <summary>
    /// Prefabから取得したスクリプト
    /// </summary>
    BloodScript bloodScript;

    /// <summary>
    /// Transformのキャッシュ
    /// </summary>
    Transform ts;

    /// <summary>
    /// 時間は毎秒60回更新を信じて定数とする
    /// </summary>
    const float dt = 1f / 60f;

    // Start is called before the first frame update
    void Start()
    {
        // 必要なものをキャッシュする
        ts = transform;
        bloodScript = particle.GetComponent<BloodScript>();

        // パーティクルをフレームごとに何個生成するか決める
        particlePerFrame = Mathf.RoundToInt(particlePerSec * dt);

        // パーティクルを生成する揺らぎ個数が
        // フレームごとに生成するパーティクルの数を超えてしまったら
        // 超えないように補正する，生成しないフレームも存在する
        if (particleFluctuationPerFrame > particlePerFrame)
        {
            particleFluctuationPerFrame = particlePerFrame;
        }
    }

    /// <summary>
    /// エミッタの生存時間が切れたら消滅する
    /// </summary>
    void JudgeEmitterLife()
    {
        if (emitterLifetime < 0f)
        {
            Destroy(this.gameObject);
        }
        emitterLifetime -= dt;
    }

    /// <summary>
    /// パーティクルの初期化を行う
    /// </summary>
    void InstantiateParticle()
    {
        GameObject p = Instantiate(particle);
        
        // コーン状にアングルを決める
        float angleX = Random.Range(-emitterAngle, emitterAngle);
        float angleY = Random.Range(-emitterAngle, emitterAngle);
        
        // モーションの種類を決める
        bool isMotion = true;
        if (driveType == ParticleDriveType.Accelaration)
        {
            isMotion = false;
        }

        float pLifetime = Random.Range(lifetime.x, lifetime.y);
        float pSpeed = Random.Range(velocity.x, velocity.y);
        float pAccel = Random.Range(accel.x, accel.y);
        float pAngularVel = Random.Range(angularVelocity.x, angularVelocity.y);

        
    }

    /// <summary>
    /// パーティクルを生成する手続き
    /// </summary>
    void EmitParticle()
    {
        // 揺らぎ個数を決める
        int fluctuation = Random.Range(0, particleFluctuationPerFrame);
        int loopCount = particlePerFrame - fluctuation;

        for (int count = 0; count < particlePerFrame; ++count)
        {
            InstantiateParticle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        JudgeEmitterLife();
        
    }
}
