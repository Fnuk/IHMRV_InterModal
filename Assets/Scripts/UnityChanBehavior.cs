using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.Windows.Speech;

public class UnityChanBehavior : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    protected PhraseRecognizer recognizer;
    protected string word = null;

    public string[] keywords = new string[] { "kick", "down", "left", "right" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

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

    int kickHash = Animator.StringToHash("kick");
    int jumpHash = Animator.StringToHash("Jump");
    int runStateHash = Animator.StringToHash("Base Layer.Locomotion");

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        //Hand hand = new Hand();
        charaController = gameObject.GetComponent<CharacterController>();

        if (recognizer != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }

    }


    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
    }

    // Update is called once per frame
    void Update()
    {
        hand = handModel.GetLeapHand();
        Debug.Log(hand + " position is : " + hand.PalmPosition);
        

        if (hand.PalmPosition.z + 1.45f * 50 > 0.4)
        {

            move = (hand.PalmPosition.z + 1.45f) * 100;
            if(move < 0)
            {
                move = move / 50;
            }


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

        Debug.Log(word);
        if (word != null)
        {
            switch (word)
            {
                case "kick":
                    anim.SetTrigger(kickHash);
                    break;
                case "down":

                    break;
                case "left":

                    break;
                case "right":

                    break;
            }
        }

    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }


}
