using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AirTilingScript : MonoBehaviour
{
    /// <summary>
    /// 空が動いているように見せかけるためのテクスチャ素材
    /// </summary>
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        var render = GetComponent<MeshRenderer>();
        material = render.material; // 事前にMaterialの情報を取得しておく
    }

    // Update is called once per frame
    void Update()
    {
        // 時間によってUV座標を動かすことで
        // 雲が動いているように見せかけるテクニック
        var offset = new Vector2(0f, -Time.time);
        material.SetTextureOffset("_MainTex", offset);
    }
}
