using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSoundEffectManagerScript : MonoBehaviour
{
    /// <summary>
    /// 足場を踏んだ時に鳴らす効果音の配列
    /// </summary>
    [SerializeField]
    List<AudioClip> clips = new List<AudioClip>();

    /// <summary>
    /// AudioSourceのキャッシュ
    /// </summary>
    AudioSource audiosc; 

    // Start is called before the first frame update
    void Start()
    {
        audiosc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 足場にタッチしたときに鳴らすための関数
    /// </summary>
    public void TouchScaffold()
    {
        // ランダムにclipsの中から1つ選ぶ
        int index = Random.Range(0, clips.Count);

        // ランダムに選んだclipの要素を鳴らす
        audiosc.PlayOneShot(clips[index]);
    }
}
