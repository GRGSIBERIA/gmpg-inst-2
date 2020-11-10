using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]    // GetComponentするときはこの1行を入れたほうがいい
[RequireComponent(typeof(AudioSource))]
public class ObstacleScript : MonoBehaviour
{
    /// <summary>
    /// ヒットされて死んだときに変えたい色
    /// </summary>
    [SerializeField]
    Material lethalMaterial;

    /// <summary>
    /// 死んだときの声
    /// </summary>
    [SerializeField]
    AudioClip[] lethalVoices;

    /// <summary>
    /// 死んだときの血しぶき
    /// </summary>
    [SerializeField]
    GameObject emitter;

    /// <summary>
    /// 死亡フラグ
    /// </summary>
    bool islethal = false;

    Transform ts;

    // Start is called before the first frame update
    void Start()
    {
        ts = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (islethal && ts.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void InstantiateEmitter(Vector3 forward)
    {
        // ベクトルを回転させて血しぶきの向きを調整する
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, forward);

        // 生成するときに座標などを指定する
        Instantiate(emitter, ts.position, rotation, ts);
    }

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーが接触して，なおかつ死亡フラグがオフのとき
        if (collision.gameObject.name == "Player" && !islethal)
        {
            // MeshRendererにMaterialの設定項目がある
            var renderer = GetComponent<MeshRenderer>();
            renderer.material = lethalMaterial;  // 現在表示されている色を怒った色に変える

            // 音を鳴らす
            var audio = GetComponent<AudioSource>();
            audio.clip = lethalVoices[Random.Range(0, lethalVoices.Length)];
            audio.Play();

            islethal = true;     // 死亡フラグをオンにする

            Vector3 direction = (collision.transform.position - ts.position).normalized;
            InstantiateEmitter(direction);
        }
    }
}
