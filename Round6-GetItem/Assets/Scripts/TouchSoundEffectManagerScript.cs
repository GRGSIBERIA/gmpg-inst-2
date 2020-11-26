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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TouchScaffold()
    {
        int index = Random.Range(0, clips.Count);
    }
}
