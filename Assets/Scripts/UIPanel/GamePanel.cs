using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    [Range(0, 120)]
    public int Hp = 120;
    public float Fp = 60;
    public int bloodCount = 0;

    private Image hpImage;
    private Image fpImage;
    private Image blood;
    private Text bloodText;
    private Button quitGame;
    private Text RoomCount;

    private void Start()
    {
        hpImage = transform.Find("State").GetChild(0).GetComponent<Image>();
        fpImage= transform.Find("State").GetChild(1).GetComponent<Image>();
        quitGame = transform.Find("GameOver").GetChild(0).GetComponent<Button>();
        blood = transform.Find("blood").GetComponent<Image>();
        bloodText = blood.transform.GetChild(0).GetComponent<Text>();
        RoomCount = transform.Find("RoomCount").GetChild(0).GetComponent<Text>();

        quitGame.onClick.AddListener(OverGame);
    }


    private void Update()
    {
        hpImage.fillAmount = Hp / 120.0f;
        fpImage.fillAmount = Fp / 60.0f;
        //Debug.Log((float)(Hp / 120.0f));
        if (Input.GetKeyDown(KeyCode.T))
        {
            bloodCount++;
            bloodText.gameObject.SetActive(true);
            bloodText.text = bloodCount.ToString();
            blood.color = new Color(255, 255, 255);
        }
        if (Input.GetKeyDown(KeyCode.Y)&&bloodCount>0)
        {
            bloodCount--;
            bloodText.text = bloodCount.ToString();
        }

        if (Hp <= 0)
        {
            quitGame.transform.parent.gameObject.SetActive(true);
        }
    }


    public void UpdateRoomCount(int count)
    {
        RoomCount.text = "剩余房间数：" + count;
    }


    private void OverGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("退出游戏");
    }

}
