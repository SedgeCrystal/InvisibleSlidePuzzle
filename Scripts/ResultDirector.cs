using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultDirector : MonoBehaviour
{
    float time;
    Text resultText;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject gameDirector = GameObject.Find("GameDirector");
        this.time = gameDirector.GetComponent<GameDirector>().time;
        Destroy(gameDirector);

        this.resultText = GameObject.Find("ResultText").GetComponent<Text>();

        resultText.text = "YourTime:" + time.ToString("F1") + "s";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene("TitleScene");
        }
        
    }
}
