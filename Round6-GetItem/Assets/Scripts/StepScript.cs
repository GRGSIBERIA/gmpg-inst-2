using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepScript : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのGameObject
    /// PrefabではなくHierarchyに存在するプレイヤーを入れておく
    /// </summary>
    GameObject player;

    /// <summary>
    /// GetComponentの呼び出しはコストがかかるので
    /// 一時変数にキャッシュする
    /// </summary>
    PlayerDrivenScript pds;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerのタグがついたGameObjectを探してくる
        // Playerは一人しかいないのでPlayer以外は見つからない
        player = GameObject.FindGameObjectWithTag("Player");

        // 何度も呼び出すのでキャッシュする
        pds = player.GetComponent<PlayerDrivenScript>();
    }

    /// <summary>
    /// プレイヤーが侵入したときに呼び出される
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        // other.gameObject.tagを使うと時間がかかる
        // other.CompareTagを使ったほうが速い
        if (other.CompareTag("Player"))
        {
            pds.StayStep();
        }
    }

    /// <summary>
    /// プレイヤーが足場を離れたときに呼び出される
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pds.ExitStep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
