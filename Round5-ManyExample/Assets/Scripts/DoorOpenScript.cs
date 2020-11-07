using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class DoorOpenScript : MonoBehaviour
{
    Animation animationComponent;

    /// <summary>
    /// ドアの開閉フラグ
    /// </summary>
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animationComponent = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ドアを開閉して，ドアの開閉フラグを反転する
    /// </summary>
    /// <param name="clipName">アニメーションクリップ名</param>
    public void GetClipAsPlay(string clipName)
    {
        // 仮引数から再生するクリップを取得して再生する
        var clip = animationComponent.GetClip(clipName);
        animationComponent.clip = clip;
        animationComponent.Play();

        // ドアの開閉フラグを反転
        isOpen = !isOpen;
    }

    public void DoorOperation()
    {
        if (isOpen)
        {
            GetClipAsPlay("DoorClose");
        }
        else
        {
            GetClipAsPlay("DoorOpen");
        }
    }
}
