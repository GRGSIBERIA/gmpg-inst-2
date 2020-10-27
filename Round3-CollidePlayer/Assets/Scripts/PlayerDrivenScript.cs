using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   // この行でRigidbodyの追加を義務付ける
public class PlayerDrivenScript : MonoBehaviour
{
    /// <summary>
    /// 加速度に対する減衰値
    /// damping > 1 減衰が大きいから遅くなる
    /// damping < 1 加速度が大きくなるのでキビキビ動く
    /// </summary>
    [SerializeField]
    float damping = 1f;

    /// <summary>
    /// Rigidbody用の変数，Inspectorから見えなくていい
    /// </summary>
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyコンポーネントを取得する
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 十字キーの入力で加速度に追加する量を決める
        var accelerator = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            accelerator += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            accelerator += Vector3.back;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            accelerator += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            accelerator += Vector3.left;
        }
        accelerator.Normalize();    // 押したキーの数でベクトルを正規化する

        // 何も入力がない場合は処理をスキップする
        // 力＝加速度＊質量について，加速度を積分
        accelerator *= damping;         // 減衰値を適用，厳密には質量に相当する
        accelerator *= Time.deltaTime;  // 微小時間に単位を合わせる
        rb.velocity += accelerator;     // 速度に合算する
        
        // 真面目に運動方程式f=maを不定積分すると案外つまらない
        // m 1/2 a**2 t ---> rb.mass * 0.5f * Vector3.Scale(accelerator, accelerator) * Time.deltaTime
        // Rigidbodyではラグランジュの運動方程式 U = mx'' + cx' + kx を自動的に解いている
        // 加速度は質量が効くと覚えよう
    }
}