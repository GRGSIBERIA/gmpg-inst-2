using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // これを追加しないとGUI関係が使えない

public class HeightManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのキャッシュ変数
    /// </summary>
    Transform player;

    /// <summary>
    /// 最大までのぼった高さ，最大登頂高度
    /// </summary>
    float maximumHeight;

    /// <summary>
    /// Textコンポーネントのキャッシュ
    /// </summary>
    Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        // メソッドチェーンでキャッシュする
        player = GameObject.FindWithTag("Player").transform;

        // 最大登頂高度を初期化
        maximumHeight = player.position.y;

        // Textコンポーネントのキャッシュ
        // UnityEngine.UIをusingしないと使えないので要注意
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが死んでいるとエラーが出る
        if (player == null)
        {
            // 最大登頂高度を取得する, 桁区切りあり，浮動小数点1桁
            string endingHeight = maximumHeight.ToString("n1");

            // お前の負け，ここまで登れたよ
            textComponent.text = "You lose.\nMaximum climbing altitude:\n" + endingHeight;

            return;     // ここで処理を止める
        }

        // 表示したいテキストを他のクラスから借りてきて呼び出す
        string text = ScaffoldScript.Get1DigitFloatString(player.position.y);

        // 文字を結合して渡してあげる
        textComponent.text = "Your Height: -50 < " + text;

        // もし，現在の高さがプレイヤーを上回ったら，最大登頂高度をアップデートする
        if (maximumHeight < player.position.y)
        {
            maximumHeight = player.position.y;
        }
    }
}
