using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.TextCore;
using TMPro.Examples;
using TMPro;

namespace CharismaSdk.Example
 
{
  
    public class ExampleScript : MonoBehaviour
    {
        [Header(header: "Charisma")]
        public AudioSource audioSource;
        public string draftToken;
        public bool showLog;
        public int storyId;
        public int storyVersion;
        [Min(1)] public int startFromScene;
        public bool useSpeech;
        public SpeechOptions speechOptions;
        [Header(header: "UI")]
        public Button button;
        public InputField inputText;
        public GameObject TextMesh;
        public Text Resultstext;
        public Button TapToConBut;
        public GameObject ResultsBackground;
        public Text CharacterText;
        public Image ChildIcon;
        public Image trainerIcon;
        public Sprite InputfieledNormal;
        public Sprite inputfieledAngry;
        public Button MicButton;
        public GameObject ARPrefab;
        public GameObject clock;
        public Sprite Angrykid;
        public Sprite HappyKid;
        public GameObject Slider;
        public GameObject ClockHandsobj;
        public Text PlayerInputResults;
        private GetPlaythroughTokenParams setting;
        private int messagenumber = 0;
        GameObject TV;
        
        public GameObject RestartCanvas;
        
        public GameObject character;
        public GameObject InputResults;


        private int _conversationId;
        private Charisma _charisma;
        private string AnimationST;
       private string textmeshtext;
        

        TapToContinueScript tapScript;
        
      

        private void Start()
        {
            Charisma.Setup();

            
            
        }
        public void Starting()
        {
            // Before we do anything, we need to set up Charisma. Put this in your initialisation code. You only need to do this one.
            //Charisma.Setup();
            

            // The Charisma logger logs events to and from Charisma.
            CharismaLogger.IsActive = showLog;
            Debug.Log("Starting Charisma");
            
            // We create the config of our token here, based on the settings we have defined in the inspector.
           
                setting = new GetPlaythroughTokenParams(storyId: storyId, storyVersion: storyVersion, draftToken: draftToken);
            

            

            // We use these settings to create a play-through token.
            Charisma.CreatePlaythroughToken(tokenParams: setting, callback: token =>
            {
              

                    // Once we receive the callback with our token, we can create a new conversation.
                    Charisma.CreateConversation(token: token, callback: conversationId =>
                    {
                    // We'll cache out conversation Id since we need  this to send replies and other events to Charisma.


                    this._conversationId = conversationId;


                    // We can now create a new charisma object and pass it our token.


                    this._charisma = new Charisma(token: token);


                    // We can now connect to Charisma. Once we receive the ready callback, we can start our play-through.
                    ConnectToCharisma();

                    });
                
            });

            // Bind the SendPlayerMessage function to the UI button.
            button.onClick.AddListener(call: SendPlayerMessage);
        }
        void ConnectToCharisma()

        {

            _charisma.Connect(onReadyCallback: () =>
            {
                // In the start function, we pass the scene we want to start from, the conversationId we cached earlier, and the speech options from the inspector. 
                _charisma.Start(sceneIndex: startFromScene, conversationId: _conversationId, speechOptions: useSpeech ? speechOptions : null);
            });

            // We can now subscribe to events from charisma.
            _charisma.OnMessage += (id, message) =>
            {
                if (useSpeech)
                {
                    // Once we have received a message, we want to play the audio. To do this we run the GetClip method and wait for the callback which contains our audio clip, then pass it to the audio player.
                    message.Message.Speech.Audio.GetClip(options: speechOptions, onAudioGenerated: (clip => { audioSource.clip = clip; audioSource.Play(); }));

                }
                //Checks for key type and takes the value
                if (message.Message.Metadata.ContainsKey("Animation"))

                {

                    message.Message.Metadata.TryGetValue("Animation", out string animationValue);

                    int animationInt = int.Parse(animationValue);

                    character = GameObject.FindGameObjectWithTag("character");

                    character.GetComponent<CharacterAnimationsScript>().animatorint = animationInt;



                }



                //deactivate/activate buttons
                if (message.Message.Metadata.ContainsKey("Micoff"))
                {
                    MicButton.interactable = false;
                    button.interactable = false;
                    inputText.interactable = false;
                }

                if (message.Message.Metadata.ContainsKey("MicOn"))
                {
                    MicButton.interactable = true;
                    button.interactable = true;
                    inputText.interactable = true;
                }



                if (message.Message.Metadata.ContainsKey("Instructions"))
                {
                    inputText.GetComponent<Animator>().Play("InputFieldEnterAnimation");
                    button.GetComponent<Animator>().Play("SEndEnter Animation");
                    MicButton.GetComponent<Animator>().Play("MicEnterAnimation");
                    
                }

                if (message.Message.Metadata.ContainsKey("Play Again"))
                {
                    //play again menu should pop up
                }
                //check if a tap repsonse is required
                if (message.TapToContinue)
                {


                    TapToConBut.gameObject.SetActive(true);
                    tapScript = TapToConBut.GetComponent<TapToContinueScript>();
                    tapScript.PlayEnterAnimation();

                }

                if (message.Message.Character.Name == "Trainer")
                {

                    ResultsBackground.GetComponent<Image>().sprite = InputfieledNormal;
                    ResultsBackground.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f);
                    CharacterText.text = message.Message.Character.Name;
                    ChildIcon.color = Color.clear;
                    trainerIcon.color = Color.white;
                    ResultsBackground.GetComponent<Animator>().Play("ResultsEnterDesktop");
                    TextMesh.GetComponent<TeleType>().AngleMultiplier = 0.0f;
                    TextMesh.GetComponent<TeleType>().SpeedMultiplier = 0.0f;
                    TextMesh.GetComponent<TeleType>().CurveScale = 0.0f;
                    StopCoroutine(TextMesh.GetComponent<TeleType>().AnimateVertexColors());
                    
                }




                if (message.Message.Character.Name == "Ben")
                {

                    PlayerInputResults.text = PlayerInputResults.text + "\r\nBen: " + message.Message.Text;
                    messagenumber++;

                    if (messagenumber > 4)
                    {
                        int index = PlayerInputResults.text.IndexOf(System.Environment.NewLine);
                        PlayerInputResults.text = PlayerInputResults.text.Substring(index + System.Environment.NewLine.Length);

                    }

                    if (message.Message.Metadata.ContainsKey("Angry"))

                    {
                        ResultsBackground.GetComponent<Image>().sprite = inputfieledAngry;
                        TextMesh.GetComponent<TeleType>().AngleMultiplier = 3.0f;
                        TextMesh.GetComponent<TeleType>().SpeedMultiplier = 1.0f;
                        TextMesh.GetComponent<TeleType>().CurveScale = 12.0f;
                        StartCoroutine(TextMesh.GetComponent<TeleType>().AnimateVertexColors());
                        ResultsBackground.GetComponent<Image>().color = new Color(1.0f, 0.5f, 0.5f);
                        CharacterText.text = message.Message.Character.Name;
                        trainerIcon.color = Color.clear;
                        ChildIcon.sprite = Angrykid;
                        ChildIcon.color = Color.white;
                        ResultsBackground.GetComponent<ResultsAnimation>().ischildSpeaking = true;
                        ResultsBackground.GetComponent<ResultsAnimation>().PlayEnterBack();
                        
                       
                        //Handheld.Vibrate();
                    }

                    else
                    {
                        ResultsBackground.GetComponent<Image>().sprite = InputfieledNormal;
                        ResultsBackground.GetComponent<Image>().color = new Color(0.9f, 0.95f, 1f);
                        CharacterText.text = message.Message.Character.Name;
                        trainerIcon.color = Color.clear;
                        ChildIcon.color = Color.white;
                        ChildIcon.sprite = HappyKid;
                        ResultsBackground.GetComponent<ResultsAnimation>().ischildSpeaking = true;
                        ResultsBackground.GetComponent<ResultsAnimation>().PlayEnterBack();
                        TextMesh.GetComponent<TeleType>().AngleMultiplier = 0.0f;
                        TextMesh.GetComponent<TeleType>().SpeedMultiplier = 0.0f;
                        TextMesh.GetComponent<TeleType>().CurveScale = 0.0f;
                        StopCoroutine(TextMesh.GetComponent<TeleType>().AnimateVertexColors());
                       
                    }


                }




                Resultstext.text = ($"{message.Message.Text}");
                TextMesh.GetComponent<TMP_Text>().text = Resultstext.text;
                StartCoroutine(TextMesh.GetComponent<TeleType>().ShowText());
                

                // If this is the end of the story, we disconnect from Charisma.
                if (message.EndStory)
                {
                    _charisma.Disconnect();
                    Instantiate(RestartCanvas, new Vector3(0, 0, 0), Quaternion.identity);
                }
            };
        }


        public void ChangeScene()
        {
            startFromScene = 2;

            _charisma.Start(sceneIndex: startFromScene, conversationId: _conversationId, speechOptions: useSpeech ? speechOptions : null);

        }

        private void Update()
        {
            if (Input.GetKeyDown(key: KeyCode.Return))
                SendPlayerMessage();
            
        }

        public void SendPlayerMessage()
        {
            if (string.IsNullOrEmpty(value: inputText.text)) return;
            

            // Send the text of our input field to Charisma.
            if (PlayerInputResults.text == "")
            {
                
                PlayerInputResults.text = PlayerInputResults.text + "Parent: " + inputText.text;
                messagenumber++;
            }
           else
            {
                messagenumber++;
                PlayerInputResults.text = PlayerInputResults.text + "\r\nParent: " + inputText.text;
                int index = PlayerInputResults.text.IndexOf(System.Environment.NewLine);

                if (messagenumber > 4)
                {
                    PlayerInputResults.text = PlayerInputResults.text.Substring(index + System.Environment.NewLine.Length);
                   
                }
            }

            InputResults.GetComponent<Animator>().Play("InputResultsEnter");
            _charisma.Reply(conversationId: _conversationId, message: inputText.text);

                inputText.text = string.Empty;
                ResultsBackground.GetComponent<Animator>().Play("ResultsExitDesktop");

        }

        public void sendTap()
        {
            _charisma.Tap(_conversationId);

            
        }

        
    }

    
}
