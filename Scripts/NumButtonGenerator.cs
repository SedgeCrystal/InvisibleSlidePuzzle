using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumButtonGenerator : MonoBehaviour
{
    public GameObject numButtonPrefab;
    GameObject canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        this.canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateNumButtons(int row,int column)
    {
        this.canvas = GameObject.Find("Canvas");
        int[] numbers = Enumerable.Range(1, row * column-1).ToArray();
        for(int i = 0; i < row*2; i++)
        {
            int i1 = UnityEngine.Random.Range(0, row * column-1);
            int i2 = UnityEngine.Random.Range(0, row * column-1);
            if(i2 ==i1)
            {
                i2++;
                if(i2 == row * column-1)
                {
                    i2 = 0;
                }
            }

            int tmp = numbers[i1];
            numbers[i1] = numbers[i2];
            numbers[i2] = tmp;
        }

        float offsetCnt_x = (row - 1) / 2.0f;
        float offsetCnt_y = ((column - 1) / 2.0f);
        
        int index = 0;
        for (int y = 0; y  < column; y++)
        {
            for (int x = 0; x < row; x++)
            {
                if (x != row - 1 || y !=column -1)
                {

                    GameObject numButton = Instantiate(this.numButtonPrefab) as GameObject;
                    NumButtonController nbController = numButton.GetComponent<NumButtonController>();
                    nbController.SetNum(numbers[index]);
                    
                    nbController.SetPosNum(index);
                    index++;
                    
                    float width = nbController.width;
                    float height = nbController.height;
                    float offset_y = nbController.offset_y;
                    numButton.transform.SetParent(this.canvas.GetComponent<RectTransform>());

                    numButton.transform.localPosition = new Vector3((x - offsetCnt_x) * width, -1 *(y - offsetCnt_y) * height +offset_y , 0);
                }
            }
        }
    }
}

