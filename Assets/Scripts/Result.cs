using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * 目的：メインゲームで取得したスコアをランキングで表示
 * 作成者：山下
 * 最終編集者
 * 
 * 編集履歴：
 * 2026/06/11 山下　作成
 */

public class Result : MonoBehaviour
{
    //メインゲームで取得したスコアを保存
    [SerializeField]
    int point;

    string[] ranking = { "１", "２", "３", "４", "５" };
    int[] rankingValue = new int[5];

    [SerializeField]
    Text[] rankingText = new Text[5];
    [SerializeField]
    Text nowScoreText;

    void Start()
    {
        //今は仮のスコアを入れてます
        point = ResultScoreTest.score;

        //ランキング呼び出し
        GetRanking();

        // ランキング書き込み
        SetRanking(point);

        //テキストの反映
        SetText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Test");
        }
    }

    void GetRanking()
    {
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    void SetRanking(int value)
    {
        for (int i = 0; i < ranking.Length; i++)
        {
            if (value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = value;
                value = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
    //ランキングリセット
    public void OnClickResetButton()
    {
        PlayerPrefs.DeleteAll();

        for (int i = 0; i < rankingValue.Length; i++)
        {
            rankingValue[i] = 0;
        }

        SetText();
    }
    void SetText()
    {
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
        }

        nowScoreText.text = point + "点!";
    }
}
