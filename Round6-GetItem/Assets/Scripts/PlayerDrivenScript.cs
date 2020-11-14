using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDrivenScript))]
public class PlayerDrivenScript : MonoBehaviour
{
    [SerializeField, Tooltip("ジャンプしたときの上向き初速 [m/s]")]
    float initialJampingVelocity;

    /// <summary>
    /// 左右の移動加速度 [m/s^2]
    /// </summary>
    [SerializeField, Tooltip("移動加速度 [m/s^2]")]
    float moveAccel;

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

    Vector3 CookMoveSpeed()
    {
        // 向きベクトル
        Vector3 direction = Vector3.zero;

        // プレイヤーのキー入力に応じて向きを変える
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        direction.Normalize();   // 向きを正規化する，同時押し対策もする

        return direction * moveAccel * Time.deltaTime;
    }

    void PlayerInput()
    {
        Vector3 moveSpeed = CookMoveSpeed();

        Vector3 gravitySpeed = Vector3.down * 9.8f * Time.deltaTime;

        Vector3 jumpSpeed = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSpeed += Vector3.up * initialJampingVelocity;
        }

        velocity += moveSpeed + jumpSpeed + gravitySpeed;
    }

    void UpdatePosition()
    {
        ts.position += velocity * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        UpdatePosition();
    }
}
