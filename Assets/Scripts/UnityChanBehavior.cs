using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class UnityChanBehavior : MonoBehaviour
{

    Animator anim;
    [SerializeField] Controller controller;
    CharacterController charaController;
    List<Hand> hands;
    public IHandModel handModel;
    Hand hand;
    Vector lastPalmPosition;

    float move;
    private Vector3 moveDirection = Vector3.forward;
    public float speed = 6.0F;
    public float gravity = 20.0F;

    int jumpHash = Animator.StringToHash("Jump");
    int runStateHash = Animator.StringToHash("Base Layer.Locomotion");

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        //Hand hand = new Hand();
        charaController = gameObject.GetComponent<CharacterController>();

        

    }

    // Update is called once per frame
    void Update()
    {
        hand = handModel.GetLeapHand();
        Debug.Log(hand + " position is : " + hand.PalmPosition);
        

        if (hand.PalmPosition.z + 1.45f * 50 > 0.4)
        {
            move = (hand.PalmPosition.z + 1.45f) * 100;
            anim.SetFloat("Speed", move);
            moveDirection = Vector3.forward*(move/100);

            //pour tourner
            
            if ( hand.PalmPosition.x*100 < - 0.4)
            {
                //Debug.Log(hand.PalmPosition.x * 100);
                float alpha = (float) System.Math.Atan(hand.PalmPosition.x / hand.PalmPosition.z);
                gameObject.transform.Rotate(0, -alpha*4, 0);
            }

            if (hand.PalmPosition.x * 100 > 0.4)
            {
                //Debug.Log(hand.PalmPosition.x * 100);
                float alpha = (float)System.Math.Atan(hand.PalmPosition.x / hand.PalmPosition.z);
                gameObject.transform.Rotate(0, -alpha*4, 0);
            }

            //pour avancer
            transform.Translate(moveDirection);
        }

        if ((hand.PalmPosition.y - 1f) > 0.2)
        {

            anim.SetTrigger(jumpHash);

        }
        else { anim.ResetTrigger(jumpHash); }



    }


}
