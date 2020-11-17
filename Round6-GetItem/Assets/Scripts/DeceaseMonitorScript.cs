using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの死亡をいちいち監視するためのスクリプト
/// </summary>
public class DeceaseMonitorScript : MonoBehaviour
{
    /// <summary>
    /// 死亡したときのParticleエフェクト
    /// </summary>
    [SerializeField]
    GameObject deceaseParticle;

    /// <summary>
    /// プレイヤーの姿勢をキャッシュする
    /// </summary>
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // タグからプレイヤーを呼び出す
        // SerializeFieldを使えばInspectorに編集できるが
        // 頻繁に更新したり，中身が変わるようなものはTagを使って直接呼び出すほうがいい
        // NOTE:
        // Windowsの再起動，ブルースクリーン等でシーンが破壊されると
        // Prefabの内容が書き変わっていてInspectorにプレイヤーを延々とアタッチし直すことがある
        // この作業はとてつもなく面倒でバグを生みやすいので，Tagを使う場合が十分にあり得る
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの一定以上の落下を検知したらプレイヤーを死亡させる
        if (player.position.y < -50f)
        {
            // カメラをプレイヤーと一緒に削除しないようにHierarchyのトップに移動する
            Camera.main.transform.parent = null;

            // プレイヤーを逝去させる
            // プレイヤーの位置にパーティクルを発生させる
            Instantiate(deceaseParticle, player.position, player.rotation);
            Destroy(player.gameObject);
        }
    }
}
