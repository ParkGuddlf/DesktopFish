using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] Sprite[] bodySprite;
    Stack<GameObject> body = new Stack<GameObject>();
    //몸은 고정 머리를 움직이는 방식으로 만들면 몸안에 길게 생성함녀된다
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spownbody();
            transform.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spownbody();
            transform.position += Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            spownbody();
            transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            spownbody();
            transform.position += Vector3.up;
        }
    }

    void spownbody()
    {
        GameObject asd = Managers.Resource.Instantiate("Charecter/CatBody", transform.parent, 1);
        asd.transform.position = transform.position;
        body.Push(asd);
    }
}
