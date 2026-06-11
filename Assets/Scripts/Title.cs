using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * 目的：タイトルシーンからメインゲームシーンへ遷移　※スコアなどのリセットでも使うかも？
 * 作成者：山下
 * 最終編集者
 * 
 * 編集履歴：
 * 2026/06/11 山下　作成
 */

public class Title : MonoBehaviour
{
    //Unityのインスペクターウィンドウでメインシーンの名前を入力
    [SerializeField] private string loadScene;

    void Start()
    {
        // フレームレートを60に固定
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(loadScene);
        }
    }
}
