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

    GameObject[] steps;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタート時に最初の足場を生成する
        GameObject firstStep = Instantiate(
            baseStep, 
            new Vector3(0f, -1.5f, 0f), 
            new Quaternion(0f, 0f, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
