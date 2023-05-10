using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clearText;
    [SerializeField] private GameObject clearTextObj;
    [SerializeField] private TextMeshProUGUI humanCountText;
    [SerializeField] private GameObject humanCountTextObj;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private GameObject playTimeTextObj;
    [SerializeField] private TextMeshProUGUI getGoldText;
    [SerializeField] private GameObject getGoldTextObj;
    [SerializeField] private GameObject goMainPageButn;
    [SerializeField] private GameObject goGameButn;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
        StartCoroutine(AppearPlayBoard());
    }

    IEnumerator AppearPlayBoard()
    {
        var one = new WaitForSeconds(1f);

        while (true)
        {
            if (img.color.a >= 1)
            {
                break;
            }
            yield return one;
        }

        clearTextObj.SetActive(true);

        yield return one;

        humanCountTextObj.SetActive(true);
        humanCountText.text = "√— ¿Œ±∏ ºˆ : " + GameManager.instance.AllHuman.Count + " ∏Ì";
        yield return one;

        playTimeTextObj.SetActive(true);

        int second = (int)Time.realtimeSinceStartup % 60;
        int minute = ((int)Time.realtimeSinceStartup / 60) % 60;
        int hour = (int)Time.realtimeSinceStartup / 3600;

        playTimeText.text = "«√∑π¿Ã ≈∏¿” : " + hour + " : " + minute + " : " + second;
        yield return one;

        getGoldTextObj.SetActive(true);
        getGoldText.text = "∞ÒµÂ »πµÊ∑Æ : " + GameManager.instance.Gold + " ∞ÒµÂ";
        yield return one;

        goMainPageButn.SetActive(true);
        goGameButn.SetActive(true);
    }

    public void GoGame()
    {
        GameManager.instance.GameStop = false;
        this.gameObject.SetActive(false);
    }

    public void GoMainPage()
    {
        LoadingSceneManager.LoadScene("MAINUI");
    }
}
