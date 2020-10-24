using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TitleCallScript : MonoBehaviour
{
    /// <summary>
    /// フェードアウトが始まる時間
    /// </summary>
    [SerializeField]
    float startFadeOutTime = 3f;

    /// <summary>
    /// フェードアウトする時間
    /// </summary>
    [SerializeField]
    float fadeOutTime = 3f;

    /// <summary>
    /// 内部時間
    /// </summary>
    float innerTime = 0f;

    /// <summary>
    /// フェードアウト中の有無を検出するためのフラグ
    /// </summary>
    bool isFadeOut = false;

    /// <summary>
    /// レンダラーを頻繁に使うので事前に確保する
    /// </summary>
    SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        // 事前にレンダラーを確保する
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 内部時間に時間増分を足す
        innerTime += Time.deltaTime;

        // 内部時間がフェードアウト
        if (innerTime > startFadeOutTime && !isFadeOut)
        {
            isFadeOut = true;   // フェードアウトを開始しよう
            innerTime = 0f;
        }
        else if (isFadeOut)  // isFadeOut == trueと同じ意味
        {
            // 時間増分値の定義
            const float deltaAlpha = 1f / (3f * 60f);

            // 少しずつ透明度を減らしていく
            var color = render.color;
            color.a -= deltaAlpha;

            if (color.a <= 0f)
            {
                // 完全に透明になったようだったら自分自身を破棄する
                Destroy(this.gameObject);
                return;
            }

            // まだ見えているので放っておく
            render.color = color;
        }
    }
}
