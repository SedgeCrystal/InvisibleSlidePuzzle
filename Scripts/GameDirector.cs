using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public int vacantNum;
    public int row;
    public int column;
    float penalty = 10.0f;
    float decreaseAlphaRatio = 0.25f;
    Text timerText;
    Text startCntText;
    public float time;
    GameObject finishButton;
    GameObject canvas;
    public GameObject penaltyTextPrefab;
    bool isStart;

    // Start is called before the first frame update
    void Awake()
    {
        this.row = 3;
        this.column = 3;
        this.isStart = false;
        

        time = -3;
        this.timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerText.text = 0.ToString("F1") + "s";
        this.startCntText = GameObject.Find("StartCountText").GetComponent<Text>();
        this.canvas = GameObject.Find("Canvas");
        

        DontDestroyOnLoad(this);

    }

    public bool CanMove(int posNum)
    {
        int x = posNum % row;
        int y = posNum / row;

        int x_vacant = vacantNum % row;
        int y_vacant = vacantNum / row;

        if (x == x_vacant)
        {
            return (y - 1) == y_vacant || (y + 1) == y_vacant;
        }
        else if (y == y_vacant)
        {
            return (x - 1) == x_vacant || (x + 1) == x_vacant;
        }
        return false;


    }

    public void decreaseAlpha()
    {
        GameObject[] numButtons = GameObject.FindGameObjectsWithTag("NumButton");
        foreach (GameObject nb in numButtons)
        {
            float a = nb.GetComponentInChildren<Text>().color.a - decreaseAlphaRatio;
            if (a < 0)
            {
                a = 0;
            }
            nb.GetComponentInChildren<Text>().color = new Color(0, 0, 0, a);
        }
    }



    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        if (this.time <= 0)
        {
            this.startCntText.text = (-1 * this.time + 0.5).ToString("F0");
        }
        else
        {
            timerText.text = time.ToString("F1")+"s";
        }

        if(!isStart && this.time > 0)
        {
            this.startCntText.text = "";
            GameObject nbGenerator = GameObject.Find("NumButtonGenerator");
            nbGenerator.GetComponent<NumButtonGenerator>().GenerateNumButtons(row, column);
            vacantNum = row * column - 1;
            isStart = true;

        }
    }

    public void OnClick_FinishButton()
    {
        if (isStart)
        {
            bool isFinish = true;
            GameObject[] numButtons = GameObject.FindGameObjectsWithTag("NumButton");
            foreach (GameObject nb in numButtons)
            {
                NumButtonController nbController = nb.GetComponent<NumButtonController>();
                if (!nbController.isCorrectPos())
                {
                    isFinish = false;
                    break;
                }
            }

            if (isFinish)
            {
                SceneManager.LoadScene("ResultScene");
            }
            else
            {
                foreach (GameObject nb in numButtons)
                {
                    nb.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
                }
                time += penalty;
                GameObject pt = Instantiate(penaltyTextPrefab) as GameObject;
                pt.transform.SetParent(this.canvas.GetComponent<RectTransform>(), false);
            }
        }
    }
}
