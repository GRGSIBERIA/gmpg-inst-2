using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerDrivenScript))]
public class PlayerDrivenScript : MonoBehaviour
{
    [SerializeField, Tooltip("ジャンプしたときの上向き初速 [m/s]"), Header("移動関連")]
    float initialJampingVelocity = 0f;

    /// <summary>
    /// 左右の移動加速度 [m/s^2]
    /// </summary>
    [SerializeField, Tooltip("移動加速度 [m/s^2]")]
    float moveAccel = 1f;

    /// <summary>
    /// 足場に足が付いているか？
    /// </summary>
    [SerializeField]
    bool isFooting = false;



    [SerializeField, Tooltip("ジャンプしたときのSE"), Header("効果音関連")]
    List<AudioClip> jumpClips = new List<AudioClip>();

    [SerializeField, Tooltip("ゲームオーバー時の音声")]
    AudioClip gameOverClip;



    /// <summary>
    /// プレイヤーが持つ速度
    /// </summary>
    Vector3 velocity = Vector3.zero;

    /// <summary>
    /// Transformのキャッシュ
    /// GetComponent<Transform>()を呼び出すのを防ぐ
    /// </summary>
    Transform ts;

    /// <summary>
    /// Rigidbodyのキャッシュ
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// AudioSourceのキャッシュ
    /// </summary>
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        ts = transform;

        rb = this.GetComponent<Rigidbody>();

        audio = this.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 足場に常駐している間に呼び出される関数
    /// 戻り地の前にpublicを付けると他のスクリプトから呼び出せるようになる
    /// publicを付けるということは，他人に公開しても良いという意味になる
    /// 安易に見知らぬ人に個人情報を提供しないように，安易にすべて公開(public)して良いものではない
    /// 副作用(公開して使われることで何が変更されるか)を理解しながらプログラミングをしよう
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

    /// <summary>
    /// 移動加速度を速度として返す関数
    /// </summary>
    /// <returns>移動速度 [m/s]</returns>
    Vector3 CookMoveSpeed()
    {
        // 向きベクトル，計算はまだだからzeroを代入してよい
        Vector3 direction = Vector3.zero;

        // プレイヤーのキー入力に応じて向きを変える
        if (Input.GetKey(KeyCode.LeftArrow))    // 左キーの入力があった
        {
            direction += Vector3.left;          // 単位ベクトルの左成分を向きに足す
        }
        if (Input.GetKey(KeyCode.RightArrow))   // 右キーの入力があった
        {
            direction += Vector3.right;         // 単位ベクトルの右成分を向きに足す
        }
        direction.Normalize();   // 向きを正規化する，同時押し対策

        // 向きに対して速度を掛けて変位を求める
        // m/s * s -> m
        return direction * moveAccel * Time.deltaTime;
    }

    /// <summary>
    /// ジャンプの掛け声を鳴らす
    /// </summary>
    void VoicingJump()
    {
        // ランダムに1つ効果音を選ぶ
        int index = Random.Range(0, jumpClips.Count);

        // 選んだAudioClipを1つ鳴らす
        audio.PlayOneShot(jumpClips[index]);
    }

    /// <summary>
    /// ジャンプした時の初速をコントロールするための関数
    /// 空中ジャンプは禁止している
    /// </summary>
    /// <returns>ジャンプしたときの初速</returns>
    Vector3 CookJumpSpeed()
    {
        Vector3 jumpSpeed = Vector3.zero;

        // スペースかつ足場に立っているならば，初速を強制的に与える
        if (Input.GetKeyDown(KeyCode.Space) && isFooting)
        {
            jumpSpeed += Vector3.up * initialJampingVelocity;
            VoicingJump();  // ジャンプしたときの音を出す
        }
        return jumpSpeed;   // 初速
    }

    void PlayerInput()
    {
        // 移動速度 [m/s]
        Vector3 moveSpeed = CookMoveSpeed();

        // ジャンプ速度 [m/s]
        rb.velocity += CookJumpSpeed();

        // 移動速度に加算する
        velocity += moveSpeed;
    }

    void UpdatePosition()
    {
        // 横方向の動きのみ物理演算を使わずに直接加算する
        ts.position += velocity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        UpdatePosition();
    }
}
