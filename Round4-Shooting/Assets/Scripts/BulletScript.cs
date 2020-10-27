using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletScript : MonoBehaviour
{
    /// <summary>
    /// 弾のスピード
    /// </summary>
    [SerializeField]
    float speed = 5.0f;

    /// <summary>
    /// 効果音の配列，ランダムに色々な声を再生させたい
    /// </summary>
    [SerializeField]
    AudioClip[] soundEffects;

    // Start is called before the first frame update
    void Start()
    {
        // Prefabを使わない場合はGameObjectをHierarchyから検索する
        // Findは数が多いと検索に時間がかかるので注意
        transform.position = GameObject.Find("Player").transform.position;

        // ランダムにサウンドエフェクトを再生する
        var source = this.GetComponent<AudioSource>();  // thisを付けないとHierarchy全体でComponentを探そうとする場合がある
        source.clip = soundEffects[Random.Range(0, soundEffects.Length)];   // SEの数でランダムにクリップを取得する
        source.Play();  // 生成された瞬間に再生する
    }

    // Update is called once per frame
    void Update()
    {
        // 真正面に向かって移動する
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
