using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechRecognition : MonoBehaviour {

    private KeywordRecognizer keywordRecognizer;
    protected PhraseRecognizer recognizer;
    protected string word = null;

    public string[] keywords = new string[] { "kick", "down", "left", "right" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    // Use this for initialization
    void Start () {

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
    void Update () {

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
