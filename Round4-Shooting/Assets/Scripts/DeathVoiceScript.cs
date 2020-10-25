using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathVoiceScript : MonoBehaviour
{
    /// <summary>
    /// デスボイスの配列
    /// </summary>
    [SerializeField]
    AudioClip[] deathVoices;

    AudioSource source;

    /// <summary>
    /// コルーチンと呼ばれる特殊な処理，処理の途中で待ってくれる
    /// デスボイスを鳴らした後に自滅する
    /// </summary>
    /// <returns>効果音の長さ</returns>
    IEnumerator PlayDeathVoiceAsDestroy()
    {
        // デスボイスを再生
        source.Play();

        // 音源が鳴り終わるまで待ってもらう
        yield return new WaitForSeconds(source.clip.length);

        // 音源が鳴り終わったら勝手に死ぬ
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 死に声を選んで再生する
        source = this.GetComponent<AudioSource>();
        source.clip = deathVoices[Random.Range(0, deathVoices.Length)];

        // 死に声を再生して，勝手に自滅する
        StartCoroutine("PlayDeathVoiceAsDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
