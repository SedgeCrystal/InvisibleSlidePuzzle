using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyTextController : MonoBehaviour
{
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.localPosition += new Vector3(0,1.5f,0);
        
        if(rect.localPosition .y> 1000)
        {
            Destroy(gameObject);
        }
    }
}
