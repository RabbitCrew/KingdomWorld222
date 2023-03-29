using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public void SceneChange(string Scene)
    {
        SceneManager.LoadScene(Scene); // Scene으로 로딩 할 씬 이름 받아와서 이동
    }

    public void GameQuit()
    {
        //Debug.Log(1234);
        Application.Quit(); // 게임 종료
    }
}