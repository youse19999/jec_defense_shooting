using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * 目的：スコアのテスト
 * 作成者：山下
 * 最終編集者
 * 
 * 編集履歴：
 * 2026/06/11 山下　作成
 */

public class ResultScoreTest : MonoBehaviour
{
    [SerializeField]
    public static int score;
    [SerializeField]
    Text text;

    [SerializeField] private string loadScene;
    void Start()
    {
        score = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            score++;
        }

        text.text = score.ToString();

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(loadScene);
        }

    }
}
