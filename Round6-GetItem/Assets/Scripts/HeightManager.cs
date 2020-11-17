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
    /// Textコンポーネントのキャッシュ
    /// </summary>
    Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        // メソッドチェーンでキャッシュする
        player = GameObject.FindWithTag("Player").transform;

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
            textComponent.text = "You lose.";
            return;     // ここで処理を止める
        }

        // 表示したいテキストを他のクラスから借りてきて呼び出す
        string text = ScaffoldScript.Get1DigitFloatString(player.position.y);

        // 文字を結合して渡してあげる
        textComponent.text = "Your Height: -50 < " + text;

    }
}
