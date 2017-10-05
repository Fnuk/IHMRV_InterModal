using UnityEngine;
using System.Collections;
using Leap;
using System;

public class PlayerScript_leap : MonoBehaviour {

    private float moveX;
    private float moveY;
    private float moveZ;

    private Controller controller;

    private Frame frame;

    private Hand hand;


    [SerializeField] private Vector handPosition;
    private Vector3 originalPosition;
    

    void Start()
    {
		controller = new Controller();

        originalPosition = transform.position;
	}

    void FixedUpdate()
    {

        frame = controller.Frame();

        if (frame.Hands.Count == 1)
        {
            hand = frame.Hands[0];

       handPosition = hand.PalmPosition;

            moveX = handPosition.x / 10f;
            moveY = handPosition.y / 20f;
            moveZ = handPosition.z / 10f;


        }

        transform.position = new Vector3(moveX + originalPosition.x, moveY, -moveZ);

    }

    void OnDisable()
    {
            controller.StopConnection();
            controller.Dispose();
    }

}
