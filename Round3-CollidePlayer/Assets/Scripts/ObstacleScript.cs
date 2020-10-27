using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]    // GetComponentするときはこの1行を入れたほうがいい
[RequireComponent(typeof(AudioSource))]
public class ObstacleScript : MonoBehaviour
{
    /// <summary>
    /// ヒットされて怒ったときに変えたい色
    /// </summary>
    [SerializeField]
    Material lethalMaterial;

    /// <summary>
    /// 死んだときの声
    /// </summary>
    [SerializeField]
    AudioClip[] lethalVoices;

    /// <summary>
    /// 怒りフラグ
    /// </summary>
    bool islethal = false;

    // Start is called before the first frame update
    void Start()
    {
        // 当たり判定だけだから何もしなくていい
    }

    // Update is called once per frame
    void Update()
    {
        // 当たり判定だけだから何もしなくていい
    }

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーが接触して，なおかつ怒りフラグがオフのとき
        if (collision.gameObject.name == "Player" && !islethal)
        {
            // MeshRendererにMaterialの設定項目がある
            var renderer = GetComponent<MeshRenderer>();
            renderer.material = lethalMaterial;  // 現在表示されている色を怒った色に変える

            var audio = GetComponent<AudioSource>();
            audio.clip = lethalVoices[Random.Range(0, lethalVoices.Length)];
            audio.Play();

            islethal = true;     // 怒りフラグをオンにする
        }
    }
}
