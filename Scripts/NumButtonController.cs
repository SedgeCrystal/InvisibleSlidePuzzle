using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumButtonController : MonoBehaviour
{
    public int num;
    int posNum;
    public float width;
    public float height;
    GameObject canvas;
    Text numText;
    GameDirector director;
    public float offset_y = 200;
    
    // Start is called before the first frame update
    void Awake()
    {
        this.canvas = GameObject.Find("Canvas");
        this.numText = GetComponentInChildren<Text>();
        
        Vector2 w_h = GetComponent<RectTransform>().sizeDelta;
        this.width = w_h.x;
        this.height = w_h.y;

        this.director = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    public void SetNum(int num)
    {
        this.num = num;

        this.numText.text = num.ToString();
    }

    public void SetPosNum(int num)
    {
        this.posNum = num;
        
    }
    // Update is called once per frame
    void Update()
    {


    }

    void ChagePos(int posNum)
    {
        int row = this.director.row;
        int column = this.director.column;
        int x = (posNum )% row;
        int y = ((posNum ) / row);
        float offsetCnt_x = (row-1)/2.0f;
        float offsetCnt_y = ((column - 1) / 2.0f);

        transform.localPosition = new Vector3((x - offsetCnt_x) * width, -1 * (y - offsetCnt_y) * height+offset_y, 0);

    }

    public void OnClick()
    {
        if (director.CanMove(this.posNum))
        {
            int tmp = director.vacantNum;
            
            director.vacantNum = this.posNum;
            this.posNum = tmp;
            
            this.ChagePos(this.posNum);
            director.decreaseAlpha();

            float a = this.numText.GetComponent<Text>().color.a;
            this.numText.GetComponent<Text>().color = new Color(0, 0, 0, 1);
        }
       
       // Destroy(gameObject);
    }
    public bool isCorrectPos()
    {
        return posNum == (num - 1);
    }
}
