using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    /// <summary>
    /// 障害物
    /// </summary>
    [SerializeField]
    GameObject obstacle;

    /// <summary>
    /// スポーンする定期的な時間
    /// </summary>
    [SerializeField]
    float spawnTime = 5f;

    float spawnCounter = 0f;

    Transform ts;

    /// <summary>
    /// スポーンする範囲
    /// </summary>
    Vector2 range;

    // Start is called before the first frame update
    void Start()
    {
        // 毎回キャッシュする
        ts = transform;
        range.x = ts.localScale.x * 0.5f;
        range.y = ts.localScale.z * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime < spawnCounter)
        {
            // 位置と向きを決める
            float x = Random.Range(-range.x, range.x);
            float z = Random.Range(-range.y, range.y);
            Vector3 position = new Vector3(x, 1f, z);
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);

            // 生成する
            Instantiate(obstacle, position, rotation);
            spawnCounter = 0f;
        }
        spawnCounter += Time.deltaTime;
    }
}
