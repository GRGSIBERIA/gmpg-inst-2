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
    /// 生成される場所の距離
    /// </summary>
    [SerializeField]
    float _distance;

    /// <summary>
    /// 踏んだ時の得点
    /// </summary>
    [SerializeField]
    int _point;

    /// <summary>
    /// 踏んだGameObject
    /// </summary>
    [SerializeField]
    GameObject _step;

    //*******************************************************
    // 勝手に内容を弄られると困るものはプロパティ宣言を利用する
    // 現状ではすべて非公開扱いになっているので外部からデータを取得したい
    // その場合はプロパティ宣言のうちgetアクセサのみ定義する
    // アクセサには取得(get)と設定(set)の2種類が存在し，それぞれただの関数になっている
    /// <summary>
    /// 難易度の名前
    /// </summary>
    /// <value>難易度の名前</value>
    public string Name { get { return _name; }}

    /// <summary>
    /// プレイヤーから離れて生成される距離
    /// </summary>
    /// <value></value>
    public float Distance { get { return _distance; }}

    /// <summary>
    /// 加算される得点のポイント
    /// </summary>
    /// <value>加算されるポイント</value>
    public int Point { get { return _point; }}

    /// <summary>
    /// 生成される足場
    /// </summary>
    /// <value>足場のプレハブ</value>
    public GameObject Step { get { return _step; }}
    //*******************************************************
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
    List<DifficultyLevel> levels = new List<DifficultyLevel>();
    // Unityでは配列[]ではなくList<>を使う

    /// <summary>
    /// 得点に対して増えていく度合
    /// </summary>
    [SerializeField]
    float basis = 10f;

    /// <summary>
    /// プレイヤーの位置を取得するためのキャッシュ
    /// </summary>
    Transform player;

    /// <summary>
    /// RotationがX:0, Y:0, Z:0のクォータニオンを返す
    /// これもキャッシュ変数
    /// </summary>
    /// <returns>オイラー角が全部ゼロのクォータニオンを返す</returns>
    Quaternion qzero = new Quaternion(0f, 0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // ゲームスタート時に最初の足場を生成する
        // 長くなりそうなときは途中で切ってもいい
        GameObject firstStep = Instantiate(     // GameObjectをHierarchyに生成
            baseStep,                           // 元になるPrefab
            new Vector3(0f, -1.5f, 0f),         // オブジェクトの位置
            qzero);                             // オブジェクトの向き

        // PlayerのTransformをキャッシュしておく
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // levelsに誰も何も設定していなかった事故が発生！
        Debug.Assert(levels.Count >= 0 || levels != null, "levelsに何も設定されていません！");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// -360～360の間でランダムに向きを生成する関数
    /// </summary>
    /// <returns>ランダムな向き</returns>
    Vector3 RandomizeDistance(float distance)
    {
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-360f, 360f), Vector3.forward);
        return rotation * Vector3.up * distance;
    }

    /// <summary>
    /// 足場を生成する関数
    /// プレイヤーの位置に応じて適当な場所へ生成する
    /// </summary>
    public void Spawn() // publicで外部に公開する宣言をする
    {
        // 1個踏むたびに各レベルの足場を1個ずつ作成する
        for (int levelId = 0; levelId < levels.Count; ++levelId)
        {
            // 長いので一時変数を作る
            DifficultyLevel lv = levels[levelId];
            
            // プレイヤーの位置からランダム
            Vector3 pos = player.position + RandomizeDistance(lv.Distance);
            Instantiate(levels[levelId].Step, pos, qzero);
        }
    }
}
