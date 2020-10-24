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
    Material angryMaterial;

    [SerializeField]
    AudioClip[] angryVoices;

    /// <summary>
    /// 怒りフラグ
    /// </summary>
    bool isAngry = false;

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
        if (collision.gameObject.name == "Player" && !isAngry)
        {
            // MeshRendererにMaterialの設定項目がある
            var renderer = GetComponent<MeshRenderer>();
            renderer.material = angryMaterial;  // 現在表示されている色を怒った色に変える

            var audio = GetComponent<AudioSource>();
            audio.clip = angryVoices[Random.Range(0, angryVoices.Length)];
            audio.Play();

            isAngry = true;     // 怒りフラグをオンにする
        }
    }
}
