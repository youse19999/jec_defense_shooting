using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerScript playerScript;

    private bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.Find("player");
        playerScript = player.GetComponent<PlayerScript>();
        isGameOver = playerScript.GetIsDead();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isGameOver);
        //プレイヤが死亡したらリザルトシーンに遷移
        if (isGameOver)
        {
            Debug.Log("プレイヤ死亡");
            //SceneManager.LoadScene("ResultScene");
        }
    }
}
