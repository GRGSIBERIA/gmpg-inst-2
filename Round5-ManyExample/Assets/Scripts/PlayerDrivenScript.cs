using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrivenScript : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    float mouseSpeed = 1f;

    Camera main;

    /// <summary>
    /// 過去のマウス座標の位置
    /// </summary>
    Vector3 prevMousePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        // メインカメラを取得する
        this.main = Camera.main;

        // この行がないとカメラの初期向きが吹っ飛ぶ
        prevMousePosition = Input.mousePosition;
    }

    void PlayerMovement()
    {
        // 向きベクトルを定義する，varで宣言すると型を自動的に推論する
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction += -transform.forward;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction += -transform.right;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }

        // 同時押しに対応するため向きベクトルを正規化する
        direction.Normalize();

        // 速度[m/s]に時間[s]を掛けると変位になる
        // 位置に変位を足し合わせると移動量になる
        transform.position += direction * speed * Time.deltaTime;
    }

    void PlayerRotation()
    {
        // 角速度をとりあえず作る
        var rotation = (Input.mousePosition - prevMousePosition) * mouseSpeed * Time.deltaTime;

        // マウスのX軸移動はプレイヤーの回転
        transform.Rotate(0f, rotation.x, 0f);

        // マウスのY軸移動はカメラの上下の振り向き
        // マウス座標とカメラ座標は違うから +/- を反転する
        main.transform.Rotate(-rotation.y, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();

        // 現在のマウス位置を過去のマウス位置に入れ替える
        prevMousePosition = Input.mousePosition;
    }
}
