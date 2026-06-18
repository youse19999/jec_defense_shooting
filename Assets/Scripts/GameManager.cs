using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;
    PlayerScript playerScript;

    private bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player");
        isGameOver = player.GetComponent<PlayerScript>().isDead;
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
