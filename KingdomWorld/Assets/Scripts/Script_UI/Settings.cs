using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public void SceneChange(string Scene)
    {
        SceneManager.LoadScene(Scene); // Scene���� �ε� �� �� �̸� �޾ƿͼ� �̵�
    }

    public void GameQuit()
    {
        //Debug.Log(1234);
        Application.Quit(); // ���� ����
    }
}