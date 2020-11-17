using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrivenScript : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    float speed = 1f;

    /// <summary>
    /// マウスの速度
    /// </summary>
    [SerializeField]
    float mouseSpeed = 1f;

    /// <summary>
    /// RaycastHitの有効距離
    /// </summary>
    [SerializeField]
    float distance = 1f;

    Camera main;

    /// <summary>
    /// 過去のマウス座標の位置
    /// </summary>
    Vector2 prevMousePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        // メインカメラを取得する
        this.main = Camera.main;

        // この行がないとカメラの初期向きが吹っ飛ぶ
        prevMousePosition = Vector2.zero;

        // プレイヤーの回転向きをゼロにする
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
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
        // Input.mousePositionプロパティも使える
        // ただし，Input.mousePositionプロパティはゲーム画面内の座標を取る
        // Vector2 rotation = (Input.mousePosition - prevMousePosition) * mouseSpeed * Time.deltaTime;

        // 角速度をとりあえず作る
        // GetAxisを使うとコントローラ操作としてマウスを認識する
        // マウスの移動を変化量として扱う
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 rotation = mouse * mouseSpeed * Time.deltaTime;

        // 現在の移動量に過去の移動量を合算する
        rotation += prevMousePosition;

        // マウスのX軸移動はプレイヤーの回転
        transform.Rotate(0f, rotation.x, 0f);

        // マウスのY軸移動はカメラの上下の振り向き
        // マウス座標とカメラ座標は違うから +/- を反転する
        main.transform.Rotate(-rotation.y, 0f, 0f);

        // マウス座標を過去のものに入れ替える
        prevMousePosition = mouse;
    }

    void RayCastHitForDoorKnob()
    {
        // 画面の中央でレイを飛ばす
        Ray ray = main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            // 特にぶつかったものはない
            if (hit.collider == null) return;

            // ドアノブじゃない場合も退場願う
            if (hit.collider.gameObject.tag != "Knob") return;

            // スペース・エンターのどちらかのキーが押されない場合はスキップ
            var isNotKeyDown = !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space));
            if (isNotKeyDown) return;

            Debug.Log("Hit");

            // 確実にドアノブっぽいので，ドアの開閉処理を移譲する
            // 親オブジェクトにドアの開閉スクリプトがついている
            var knob = hit.collider.gameObject;;
            knob.GetComponentInParent<DoorOpenScript>().DoorOperation();
        }

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();
        RayCastHitForDoorKnob();
    }
}
