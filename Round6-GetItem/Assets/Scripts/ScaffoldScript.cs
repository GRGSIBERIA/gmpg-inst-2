using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldScript : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        // .(ドット)演算子をつなげて書く方法をメソッドチェーンと呼ぶ
        pds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDrivenScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
