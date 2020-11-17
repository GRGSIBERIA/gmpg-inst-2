using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 難易度を格納するためのクラス
/// 配列の中にクラスを入れてInspectorに表示するには
/// System.Serializableをクラス宣言の前に書いておく
/// </summary>
[System.Serializable]
public class DifficultyLevel
{
    /// <summary>
    /// 難易度の名前
    /// </summary>
    [SerializeField]
    string _name;

    /// <summary>
    /// プレイヤーとの距離
    /// </summary>
    [SerializeField]
    float _distance;
    
    /// <summary>
    /// 加点スコア
    /// </summary>
    [SerializeField]
    int _point;

    /// <summary>
    /// ステップの色
    /// </summary>
    [SerializeField]
    Material _colorMaterial;

    //******************************************************
    /// プロパティの宣言, プロパティとは取得(get)，設定(set)用の関数の別名
    /// _name, _distance, _pointは誰かに書き換えて欲しくない
    /// しかし，それぞれ取得だけはできて欲しい場合にgetアクセサを使ってプロパティ宣言をすると
    /// 取得はできるが外部から変更が不可能なプログラムが書ける
    /// 外部からの変更を禁止することで，バグの発生個所を狭めることができる
    public string Name { get { return _name; }}

    public float Distance { get { return _distance; }}

    public int Point { get { return _point; }}

    public Material ColorMaterial { get { return _colorMaterial; }}
    //******************************************************

    /// <summary>
    /// クラス名の関数はコンストラクタとして扱われる
    /// </summary>
    /// <param name="name">難易度の名前</param>
    /// <param name="distance">スポーンされる距離</param>
    /// <param name="point">得点</param>
    /// <param name="material">色</param>
    public DifficultyLevel(string name, float distance, int point, Material material)
    {
        
    }
}

/// <summary>
/// 足場を生成するためのスクリプト
/// </summary>
public class SpawnerScript : MonoBehaviour
{
    /// <summary>
    /// 足場の基本となるプレファブ
    /// </summary>
    [SerializeField]
    GameObject baseStep;

    /// <summary>
    /// 難易度の配列
    /// </summary>
    [SerializeField]
    DifficultyLevel[] levels;

    /// <summary>
    /// 得点に対して増えていく度合
    /// </summary>
    [SerializeField]
    float basis = 10f;

    /// <summary>
    /// プレイヤーの位置を取得するためのキャッシュ
    /// </summary>
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタート時に最初の足場を生成する
        GameObject firstStep = Instantiate(     // GameObjectをHierarchyに生成
            baseStep,                           // 元になるPrefab
            new Vector3(0f, -1.5f, 0f),         // オブジェクトの位置
            new Quaternion(0f, 0f, 0f, 0f));    // オブジェクトの向き

        // PlayerのTransformをキャッシュしておく
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 誰も何も設定していなかった事故が発生！
        if (levels.Length <= 0)
        {
            levels = new DifficultyLevel[] {  // 配列はnewで初期化しなければならない
                new DifficultyLevel(    // new でコンストラクタ呼び出し
                    "Normal", 10f, 1,   // 距離と得点の順番
                    new Material(Shader.Find("Standard")) // 標準シェーダを探してマテリアルに貼り付ける
                )
            };  // ネスト(入れ子)が深くなる初期化はこのようにインデントを使って読みやすく工夫する
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 足場を生成する関数
    /// プレイヤーの位置に応じて適当な場所へ生成する
    /// </summary>
    void Spawn()
    {
        
    }
}
