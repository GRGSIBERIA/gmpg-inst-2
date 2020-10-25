using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitterScript : MonoBehaviour
{
    [SerializeField]
    float enemyInstantiateTimingMaximum = 5f;

    [SerializeField]
    float enemyInstantiateTimingMinimum = 3f;

    [SerializeField]
    GameObject enemy;

    /// <summary>
    /// オブジェクト内の時間
    /// </summary>
    float time;

    /// <summary>
    /// 敵を生成するタイミング
    /// </summary>
    float instantiateTiming;

    /// <summary>
    /// 生成タイミングを取得する
    /// </summary>
    /// <returns>生成タイミング[秒]</returns>
    float GetTiming()
    {
        return Random.Range(enemyInstantiateTimingMinimum, enemyInstantiateTimingMaximum);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期値のチェックを一応やっておく
        Debug.Assert(enemyInstantiateTimingMaximum >= enemyInstantiateTimingMinimum);

        // 生成タイミングの初期値を代入する
        instantiateTiming = GetTiming();
    }

    // Update is called once per frame
    void Update()
    {
        // 時間増分を内部時間に足す
        time += Time.deltaTime;

        if (time > instantiateTiming)
        {
            // 適当な位置に敵を出現させる
            var enemy = Instantiate(this.enemy);
            enemy.transform.position = new Vector3(Random.Range(-0.9f, 0.9f), 0f, 15f);

            // 内部時間をリセットする
            time = 0f;
            instantiateTiming = GetTiming();
        }
    }
}
