﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldScript : MonoBehaviour
{
    /// <summary>
    /// 踏んでしまった足場の色
    /// </summary>
    [SerializeField]
    Material steppedMaterial;

    /// <summary>
    /// 自滅する時間
    /// </summary>
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

    /// <summary>
    /// 自分自身が削除されたらspawnerに知らせたい
    /// </summary>
    SpawnerScript spawner;

    /// <summary>
    /// 浮動小数点数が1桁の文字を取得する関数
    /// </summary>
    /// <returns>浮動小数点数が1桁の文字列</returns>
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
        spawner = GameObject.Find("Spawner").GetComponent<SpawnerScript>();

        // 親オブジェクトにMeshRendererが存在する
        render = transform.parent.GetComponent<MeshRenderer>();

        // 子オブジェクトにテキストがいる
        text = transform.GetChild(0).GetComponent<TextMesh>();
        text.text = Get1DigitFloatString(); // テキストに自分の秒数を入れる
    }

    // Update is called once per frame
    void Update()
    {
        // すでに足場を利用していたら自爆タイマーを発動する
        if (isStepped)
        {
            selfDestructTime -= Time.deltaTime; // タイマーを更新にかかった時間だけ減らす
            text.text = Get1DigitFloatString(); // 文字の中身を現在の時間に更新する
        }

        // 自爆タイマーがゼロ以下になったら足場を消す
        if (selfDestructTime < 0f)
        {
            // 足場本体は，Scaffoldの親 = Step
            // Stepを削除するとScaffoldも削除される
            Destroy(ts.parent.gameObject);
            spawner.Spawn();
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
            render.material = steppedMaterial;
        }
    }

    /// <summary>
    /// プレイヤーが足場に引っかかっているか判定する
    /// OnTrigger～を使うとすり抜けを実装できる
    /// </summary>
    /// <param name="other">当たり判定の対象</param>
    void OnTriggerStay(Collider other)
    {
        // NOTE:
        // other.gameObject.tag == "Player" とするよりも
        // other.CompareTag("Player")を使ったほうがとても速い

        // プレイヤーが当たり判定の場所に入ってきたら
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
        // プレイヤーが当たり判定の場所から出たら
        if (other.CompareTag("Player"))
        {
            // 足場から離れた
            pds.ExitStep();
        }
    }
}
