using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DoorOpenScript : MonoBehaviour
{
    [SerializeField]
    AudioClip doorOpenSE;

    [SerializeField]
    AudioClip doorCloseSE;

    Animator animationComponent;

    AudioSource audioSource;

    /// <summary>
    /// ドアの開閉フラグ
    /// </summary>
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animationComponent = this.GetComponent<Animator>();

        // 自動再生を防ぐための仕組み
        animationComponent.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySoundEffect()
    {
        if (isOpen)
        {
            audioSource.clip = doorCloseSE;
            audioSource.PlayDelayed(0.1f);
        }
        else
        {
            audioSource.clip = doorOpenSE;
            audioSource.Play();
        }
    }

    /// <summary>
    /// ドアを開閉して，ドアの開閉フラグを反転する
    /// </summary>
    /// <param name="clipName">アニメーションクリップ名</param>
    IEnumerator GetClipAsPlay(string clipName)
    {
        // 仮引数から再生するクリップを取得して再生する
        animationComponent.enabled = true;
        animationComponent.Play(clipName);
        PlaySoundEffect();

        // defaultレイヤーのクリップについて再生秒数を取得してその時間だけ待機する
        var seconds = animationComponent.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(seconds);

        // ドアの開閉フラグを反転
        isOpen = !isOpen;
        animationComponent.enabled = false; // 自動再生防止装置
    }

    public void DoorOperation()
    {
        // コルーチンを呼び出して指定秒数だけアニメーションを再生させる
        if (isOpen)
        {
            StartCoroutine("GetClipAsPlay", "DoorClose");
        }
        else
        {
            StartCoroutine("GetClipAsPlay", "DoorOpen");
        }
    }
}
