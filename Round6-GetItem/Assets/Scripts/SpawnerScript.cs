using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 足場を生成するためのスクリプト
/// </summary>
public class SpawnerScript : MonoBehaviour
{
    /// <summary>
    /// 足場の基本となるプレファブ
    /// </summary>
    [SerializeField]
    GameObject baseStep;

    /// <summary>
    /// 得点に対して増えていく度合
    /// </summary>
    [SerializeField]
    float basis = 10f;

    /// <summary>
    /// プレイヤーの位置を取得するためのキャッシュ
    /// </summary>
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタート時に最初の足場を生成する
        GameObject firstStep = Instantiate(     // GameObjectをHierarchyに生成
            baseStep,                           // 元になるPrefab
            new Vector3(0f, -1.5f, 0f),         // オブジェクトの位置
            new Quaternion(0f, 0f, 0f, 0f));    // オブジェクトの向き

        // PlayerのTransformをキャッシュしておく
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 足場を生成する関数
    /// プレイヤーの位置に応じて適当な場所へ生成する
    /// </summary>
    void Spawn()
    {

    }
}
