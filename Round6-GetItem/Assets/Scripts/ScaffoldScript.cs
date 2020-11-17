using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldScript : MonoBehaviour
{
    [SerializeField]
    Material usedMaterial;

    [SerializeField]
    float selfDestructTime = 10f;

    /// <summary>
    /// プレイヤーのスクリプトから呼び出したい関数がある
    /// StayStep, ExitStepの2種類
    /// </summary>
    PlayerDrivenScript pds;

    /// <summary>
    /// この足場はもう踏まれたか？
    /// デフォルトはfalse
    /// </summary>
    bool isStepped = false;

    /// <summary>
    /// 親オブジェクトのメッシュのキャッシュ
    /// </summary>
    MeshRenderer render;

    /// <summary>
    /// 子オブジェクトのTextMeshコンポーネントのキャッシュ
    /// </summary>
    TextMesh text;

    /// <summary>
    /// Transformのキャッシュ
    /// </summary>
    Transform ts;

    string Get1DigitFloatString()
    {
        return string.Format("{0:f1}", selfDestructTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        ts = transform;

        // .(ドット)演算子をつなげて書く方法をメソッドチェーンと呼ぶ
        // 戻り値がオブジェクトであればたいていはメソッドチェーンができる
        pds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDrivenScript>();

        // 親オブジェクトにMeshRendererが存在する
        render = transform.parent.GetComponent<MeshRenderer>();

        // 子オブジェクトにテキストがいる
        text = transform.GetChild(0).GetComponent<TextMesh>();
        text.text = Get1DigitFloatString();
    }

    // Update is called once per frame
    void Update()
    {
        // すでに足場を利用していたら自爆タイマーを発動する
        if (isStepped)
        {
            selfDestructTime -= Time.deltaTime;
            text.text = Get1DigitFloatString();
        }

        if (selfDestructTime < 0f)
        {
            Destroy(ts.parent.gameObject);
        }
    }

    /// <summary>
    /// プレイヤーが足場に侵入したか判定する
    /// </summary>
    /// <param name="other">当たり判定の対象</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 使用済みの足場であることを記憶する
            isStepped = true;
            render.material = usedMaterial;
        }
    }

    /// <summary>
    /// プレイヤーが足場に引っかかっているか判定する
    /// OnTrigger～を使うとすり抜けを実装できる
    /// </summary>
    /// <param name="other">当たり判定の対象</param>
    void OnTriggerStay(Collider other)
    {
        // other.gameObject.tag == "Player" とするよりも
        // other.CompareTag("Player")を使ったほうがとても速い
        if (other.CompareTag("Player"))
        {
            // 足場に引っかかっている
            pds.StayStep();
        }
    }

    /// <summary>
    /// プレイヤーが足場から抜けた瞬間を判定する
    /// </summary>
    /// <param name="other">当たり判定の対象</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 足場から離れた
            pds.ExitStep();
        }
    }
}
