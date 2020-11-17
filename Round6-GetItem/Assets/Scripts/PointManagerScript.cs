using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManagerScript : MonoBehaviour
{
    /// <summary>
    /// 初期ポイント
    /// </summary>
    int point = 0;

    /// <summary>
    /// ポイント倍率！
    /// </summary>
    [SerializeField]
    int multiply = 1;

    /// <summary>
    /// Textのキャッシュ
    /// </summary>
    Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // ToStringを使うと指定した書式で文字列に変換できる
        // N=数値, 0=小数点の桁数は0桁
        textComponent.text = "Point: " + point.ToString("N0");
    }

    /// <summary>
    /// ポイントを追加する
    /// </summary>
    /// <param name="pt">追加するポイント</param>
    public void AddPoint(int pt)
    {
        point += pt * multiply;    // ポイントを追加
        ++multiply;                // ポイント倍率を追加
    }
}
