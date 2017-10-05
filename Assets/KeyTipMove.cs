using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTipMove : MonoBehaviour
{
    private GameObject part7;

    // Use this for initialization
    void Start()
    {
        part7 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up")) part7.transform.Translate(Vector3.left*0.07f);
        if (Input.GetKey("left")) part7.transform.Translate(Vector3.back * 0.07f);
        if (Input.GetKey("right")) part7.transform.Translate(Vector3.forward * 0.07f);
        if (Input.GetKey("down")) part7.transform.Translate(Vector3.right * 0.07f);
        if (Input.GetKey("PageUp")) part7.transform.Translate(Vector3.up * 0.07f);
        if (Input.GetKey("PageDown")) part7.transform.Translate(Vector3.down * 0.07f);
    }
}