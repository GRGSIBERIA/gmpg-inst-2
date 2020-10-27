using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonosubaBurstScript : MonoBehaviour
{
    /// <summary>
    /// このすばエフェクトのPrefabを指定する
    /// </summary>
    [SerializeField]
    GameObject konosuba;

    /// <summary>
    /// カメラから見たときの深さを指定する
    /// </summary>
    [SerializeField]
    float depth = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 0: 右クリック（Primary）
        // 1: 左クリック（Secondary）
        // 2: 中ボタンクリック
        // マウスの設定が左右逆になることもあるので注意
        if (Input.GetMouseButtonDown(0))
        {
            // エフェクトを生成する
            var knsb = Instantiate(konosuba);

            // マウスをクリックしたときの座標を得る（カメラがPerspectiveの場合）
            var mouse = Input.mousePosition;
            mouse.z = depth;    // 表示される深さを設定する

            // クリックされたスクリーン座標をワールド座標に変換する
            var pos = Camera.main.ScreenToWorldPoint(mouse);
          
            // そこへエフェクトを配置する
            knsb.transform.position = pos;
        }

        // カメラの種類によって変換方法が変わる
        // スクリーン座標とワールド座標の変換はこのサイトがわかりやすい
        // https://code.hildsoft.com/entry/2017/07/04/081214
    }
}
