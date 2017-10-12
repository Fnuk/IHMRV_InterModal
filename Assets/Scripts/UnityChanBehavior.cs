using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class UnityChanBehavior : MonoBehaviour
{

    Animator anim;
    CharacterController charaController;
    [SerializeField] IHandModel leftHandModel, rightHandModel;
    Hand activeHand;
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
        if (leftHandModel.isActiveAndEnabled == true)
            activeHand = leftHandModel.GetLeapHand();
        if(rightHandModel.isActiveAndEnabled == true)
            activeHand = rightHandModel.GetLeapHand();
        if (rightHandModel.isActiveAndEnabled == false && leftHandModel.isActiveAndEnabled == false)
            activeHand = null;

        //Debug.Log(activeHand + " position is : " + activeHand.PalmPosition);
        Debug.Log(activeHand);
        
        if(activeHand == null)
            anim.SetFloat("Speed", 0f);
        else if (activeHand.PalmPosition.z + 1.45f * 50 > 0.4)
        {
            move = (activeHand.PalmPosition.z + 1.45f) * 100;
            anim.SetFloat("Speed", move);
            moveDirection = Vector3.forward*(move/100);

            //pour tourner
            
            if (activeHand.PalmPosition.x*100 < - 0.4)
            {
                //Debug.Log(hand.PalmPosition.x * 100);
                float alpha = (float) System.Math.Atan(activeHand.PalmPosition.x / activeHand.PalmPosition.z);
                gameObject.transform.Rotate(0, -alpha*4, 0);
            }

            if (activeHand.PalmPosition.x * 100 > 0.4)
            {
                //Debug.Log(hand.PalmPosition.x * 100);
                float alpha = (float)System.Math.Atan(activeHand.PalmPosition.x / activeHand.PalmPosition.z);
                gameObject.transform.Rotate(0, -alpha*4, 0);
            }

            //pour avancer
            if (moveDirection.z > 0)
                transform.Translate(moveDirection);
            else
                transform.Translate(Vector3.back*0.02f);
        }

        if ((activeHand.PalmPosition.y - 1f) > 0.2)
        {

            anim.SetTrigger(jumpHash);

        }
        else { anim.ResetTrigger(jumpHash); }



    }


}
