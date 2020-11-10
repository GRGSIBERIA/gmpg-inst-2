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
    Vector2 accel = new Vector2();

    /// <summary>
    /// 角速度の幅
    /// </summary>
    [SerializeField]
    Vector2 angularVelocity;

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
    /// フレームあたりに発生するパーティクル数の揺らぎ
    /// </summary>
    [SerializeField]
    int particleFluctuationPerFrame = 4;

    /// <summary>
    /// 時間は毎秒60回更新を信じて定数とする
    /// </summary>
    const float dt = 1f / 60f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
