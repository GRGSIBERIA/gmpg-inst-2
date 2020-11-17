using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの死亡をいちいち監視するためのスクリプト
/// </summary>
public class DeceaseMonitorScript : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの姿勢をキャッシュする
    /// </summary>
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // タグからプレイヤーを呼び出す
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの一定以上の落下を検知したらプレイヤーを死亡させる
        if (player.position.y < -50f)
        {
            // プレイヤーを逝去させる
            Destroy(player);
        }
    }
}
