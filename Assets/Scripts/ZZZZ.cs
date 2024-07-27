using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZZZ : MonoBehaviour
{
    [SerializeField]
    int sizeStep = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && sizeStep < 5 && sizeStep> -1)
        {
            sizeStep++;
            transform.position += new Vector3(0, 0.5f, 0);
            transform.localScale += Vector3.one;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && sizeStep < 6 && sizeStep > 0)
        {
            sizeStep--;
            transform.position -= new Vector3(0, 0.5f, 0);
            transform.localScale -= Vector3.one;
        }
    }
}
