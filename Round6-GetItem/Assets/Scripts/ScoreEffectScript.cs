using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffectScript : MonoBehaviour
{
    /// <summary>
    /// パーティクルのキャッシュ
    /// </summary>
    ParticleSystem ps;

    /// <summary>
    /// マテリアルの隠れプロパティ
    /// </summary>
    Material _mat;

    public Material ParticleMaterial { set { _mat = value; }}

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
