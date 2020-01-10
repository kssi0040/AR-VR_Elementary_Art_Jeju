using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;


public class TextManager : MonoBehaviour
{
    public int currentStage;
    public int count = 0;
    public string text;
    
    [SerializeField]
    int tempInt = 0;

    public RectTransform Link;
    public RectTransform Controller;
    public RectTransform LinkCanvas;
    public RectTransform ImgLinkCanvas;
    public RectTransform Typing1Canvas;
    public RectTransform Typing2Canvas;
    public RectTransform OptionCanvas;
    public RectTransform ImgOptionCanvas;
    public RectTransform GameCanvas;

    public Transform sphere;
    public Transform CH1_nomal;
    public Transform CH2_nomal;
    public Transform CH3;
    
    public Image BackGround;
    public Image PopUp;
    public Image Highlight;
    public Image sphereIcon;
    public Image Masterpiece;
    public RawImage TextBox;
    public RawImage NPCTextBox;

    public Text str;
    public Text PopUpText;
    public Text NPCText;
    
    public Animator CHS;
    public Animator NPC;
    public Animator ArtSlide;
    public Canvas canvas;

    public RectTransform CharacterScroll;
    public RectTransform PopUpScroll;
    public RectTransform NPCScroll;

    public VideoPlayer video;
    LinkQuizManager linkQuizManager;
    public Camera puzzleCamera;
    public Camera main;
    public GameControl Puzzle;
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        linkQuizManager = FindObjectOfType<LinkQuizManager>();

        LinkCanvas.transform.gameObject.SetActive(false);
        ImgLinkCanvas.transform.gameObject.SetActive(false);
        Typing1Canvas.transform.gameObject.SetActive(false);
        Typing2Canvas.transform.gameObject.SetActive(false);
        OptionCanvas.transform.gameObject.SetActive(false);
        ImgOptionCanvas.transform.gameObject.SetActive(false);


        Link.transform.gameObject.SetActive(false);
        PopUp.transform.gameObject.SetActive(false);
        PopUpText.transform.gameObject.SetActive(false);
        PopUpScroll.transform.gameObject.SetActive(false);

        CH1_nomal.transform.gameObject.SetActive(false);
        CH2_nomal.transform.gameObject.SetActive(false);
        CH3.transform.gameObject.SetActive(true);

        CHS.SetTrigger("teachAnim");

        text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
        if(text.Contains("###"))
        {
            string temp = text.Replace("###", AppManage.Instance.Name);
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
        }
        else
        {
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
        }
        switch(currentStage)
        {
            case 1:
                Controller.transform.gameObject.SetActive(false);
                sphere.transform.gameObject.SetActive(false);
                sphereIcon.transform.gameObject.SetActive(false);
                NPC.transform.gameObject.SetActive(false);
                NPCText.text = string.Empty;
                NPCTextBox.transform.gameObject.SetActive(false);
                NPCScroll.transform.gameObject.SetActive(false);
                Masterpiece.transform.gameObject.SetActive(false);
                Puzzle.pazzle_control.transform.gameObject.SetActive(false);
                GameCanvas.transform.gameObject.SetActive(false);
                break;
            case 2:
                Controller.transform.gameObject.SetActive(false);
                sphere.transform.gameObject.SetActive(false);
                sphereIcon.transform.gameObject.SetActive(false);
                NPC.transform.gameObject.SetActive(false);
                NPCText.text = string.Empty;
                NPCTextBox.transform.gameObject.SetActive(false);
                NPCScroll.transform.gameObject.SetActive(false);
                Masterpiece.transform.gameObject.SetActive(false);
                ArtSlide.transform.gameObject.SetActive(false);
                break;
            case 3:
                Controller.transform.gameObject.SetActive(false);
                sphere.transform.gameObject.SetActive(false);
                sphereIcon.transform.gameObject.SetActive(false);
                NPC.transform.gameObject.SetActive(false);
                NPCText.text = string.Empty;
                NPCTextBox.transform.gameObject.SetActive(false);
                NPCScroll.transform.gameObject.SetActive(false);
                break;
            case 4:
                Controller.transform.gameObject.SetActive(false);
                sphere.transform.gameObject.SetActive(false);
                sphereIcon.transform.gameObject.SetActive(false);
                NPC.transform.gameObject.SetActive(false);
                NPCText.text = string.Empty;
                NPCTextBox.transform.gameObject.SetActive(false);
                NPCScroll.transform.gameObject.SetActive(false);
                break;
            case 5:
                NPC.transform.gameObject.SetActive(false);
                NPCText.text = string.Empty;
                NPCTextBox.transform.gameObject.SetActive(false);
                NPCScroll.transform.gameObject.SetActive(false);
                Masterpiece.transform.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    AnimatorStateInfo animInfo;
    // Update is called once per frame
    void Update()
    {
        switch (currentStage)
        {
            case 1:
                switch (count)
                {
                    default:
                        break;
                }
                break;
            case 2:
                switch(count)
                {
                    default:
                        break;
                }
                break;
            case 3:
                switch(count)
                {
                    case 1:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
            case 4:
                switch(count)
                {
                    default:
                        break;
                }
                break;
            case 5:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
        }
    }
    
    
    public void forwardDown()
    {
        Text tempText;
        
        if (AppManage.Instance.isComplite)
        {
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(false);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 1:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            PopUp.sprite = Resources.Load<Sprite>("Popup_Png");
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.visitjeju.net/kr/detail/view?contentsid=CNTS_000000000020199&menuId=DOM_000001718003000000#p1");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "문화예술카페";
                            count++;
                            break;
                        case 2:
                            Link.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 3:
                            PopUpScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 4:
                        case 6:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 5:
                        case 7:
                        case 12:
                        case 23:
                        case 27:
                        case 33:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 8:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            BackGround.sprite=Resources.Load<Sprite>("이중섭생가BG");
                            count++;
                            break;
                        case 9:
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 10:
                            NPC.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUp.sprite = Resources.Load<Sprite>("이중섭소의말");
                            NPCText.text = string.Empty;
                            count++;
                            break;
                        case 11:
                            PopUp.sprite = Resources.Load<Sprite>("Popup_Png");
                            PopUp.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 14:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("이중섭_황소");
                            Masterpiece.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 15:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            Masterpiece.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 16:
                            if(linkQuizManager.OptionClear==false)
                            {
                                NPC.transform.gameObject.SetActive(false);
                                NPCText.transform.gameObject.SetActive(false);
                                NPCTextBox.transform.gameObject.SetActive(false);
                                NPCScroll.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.OptionClear = false;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                NPCText.text = string.Empty;
                                NPCText.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 17:
                            video.Play();
                            count++;
                            break;
                        case 18:
                            video.time = 0.0f;
                            video.Stop();
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 22:
                            BackGround.sprite = Resources.Load<Sprite>("생가내부");
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 25:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("familyAndArtist");
                            Masterpiece.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 26:
                            NPC.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            Masterpiece.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 30:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 31:
                            if(linkQuizManager.OptionClear==false)
                            {
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                NPC.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                                CH3.transform.gameObject.SetActive(false);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH2_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.OptionClear = false;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(true);
                                NPC.transform.gameObject.SetActive(true);
                                NPCTextBox.transform.gameObject.SetActive(true);
                                NPCScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 34:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("family6");
                            Masterpiece.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 35:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH2_nomal.transform.gameObject.SetActive(true);
                                CHS.SetTrigger("CH2Anim");
                            }
                            else
                            {
                                CH1_nomal.transform.gameObject.SetActive(true);
                                CHS.SetTrigger("CH1Anim");
                            }
                            NPC.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            Masterpiece.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 36:
                            Debug.Log("Puzzle.step00: " + Puzzle.step);
                            //퍼즐
                            if (Puzzle.step != GameControl.STEP.CLEAR)
                            {
                                puzzleCamera.transform.gameObject.SetActive(true);
                                canvas.worldCamera = puzzleCamera;
                                main.transform.gameObject.SetActive(false);
                                Puzzle.pazzle_control.transform.gameObject.SetActive(true);
                                NPC.transform.gameObject.SetActive(false);
                                CH1_nomal.transform.gameObject.SetActive(false);
                                CH2_nomal.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                str.transform.gameObject.SetActive(false);
                                GameCanvas.transform.gameObject.SetActive(true);
                                BackGround.sprite = Resources.Load<Sprite>("Dim");
                                Highlight.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                Debug.Log("Puzzle.step: " + Puzzle.step);
                                main.transform.gameObject.SetActive(true);
                                canvas.worldCamera = main;
                                puzzleCamera.transform.gameObject.SetActive(false);
                                Puzzle.pazzle_control.step = PazzleControl.STEP.NONE;
                                Puzzle.step = GameControl.STEP.PLAY;
                                Debug.Log("Puzzle.step: " + Puzzle.step);
                                Puzzle.OnRetryButtonPush();
                                Puzzle.pazzle_control.transform.gameObject.SetActive(false);
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                GameCanvas.GetChild(1).gameObject.SetActive(true);
                                GameCanvas.GetChild(2).gameObject.SetActive(false);
                                GameCanvas.transform.gameObject.SetActive(false);
                                BackGround.sprite = Resources.Load<Sprite>("생가내부");
                                Highlight.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 38:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 39:
                            SceneManager.LoadScene("Stage1_End");
                            break;
                        default:
                            count++;
                            break;
                           
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(false);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 1:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 2:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 9:
                            NPC.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            Masterpiece.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("정미진작가 게와 아이들");
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 10:
                            if(linkQuizManager.ImgOptionClear==false)
                            {
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                Masterpiece.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateImgOptionQuiz(1, tempInt);
                                ImgOptionCanvas.transform.gameObject.SetActive(true);
                                CH3.transform.gameObject.SetActive(false);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH2_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                ImgOptionCanvas.transform.gameObject.SetActive(false);
                                str.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                NPC.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 11:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("crabAndChild");
                            Masterpiece.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 12:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 13:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("twoChildren");
                            Masterpiece.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 14:
                            Masterpiece.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 17:
                            if(linkQuizManager.ImgLinkClear==false)
                            {
                                NPC.transform.gameObject.SetActive(false);
                                NPCText.transform.gameObject.SetActive(false);
                                NPCTextBox.transform.gameObject.SetActive(false);
                                NPCScroll.transform.gameObject.SetActive(false);
                                ImgLinkCanvas.transform.gameObject.SetActive(true);
                                linkQuizManager.GenerateImgLinkQuiz(1, tempInt);
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.ImgLinkClear = false;
                                ImgLinkCanvas.transform.gameObject.SetActive(false);
                                NPC.transform.gameObject.SetActive(true);
                                NPCText.transform.gameObject.SetActive(true);
                                NPCText.text = string.Empty;
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 18:
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 19:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("crabAndChild");
                            Masterpiece.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 20:
                            Masterpiece.sprite = Resources.Load<Sprite>("twoChildren");
                            count++;
                            break;
                        case 21:
                            if(linkQuizManager.OptionClear==false)
                            {
                                PopUp.transform.gameObject.SetActive(false);
                                PopUpText.transform.gameObject.SetActive(false);
                                PopUpScroll.transform.gameObject.SetActive(false);
                                Masterpiece.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateOptionQuiz(1, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.OptionClear = false;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                PopUpText.text = string.Empty;
                                PopUpText.transform.gameObject.SetActive(true);
                                NPC.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 22:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            NPC.SetTrigger("leeJuongSub_Smile");
                            count++;
                            break;
                        case 25:
                            NPC.SetTrigger("leeJuongSub_Idle");
                            count++;
                            break;
                        case 30:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            //작품 애니메이션 켜기
                            ArtSlide.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 31:
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            ArtSlide.transform.gameObject.SetActive(false);
                            //작품 애니메이션 끄기
                            count++;
                            break;
                        case 32:
                            SceneManager.LoadScene("Stage2_End");
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 0:
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 1:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            count++;
                            break;
                        case 5:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            GameObject.Find("WebManager").SendMessage("initURL", "https://culture.seoqwipo.go.kr/jslee/");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "이중섭 미술관";
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH2_nomal.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.transform.gameObject.SetActive(false);
                            }
                            CH3.transform.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 6:
                            if(linkQuizManager.ImgOptionClear==false)
                            {
                                PopUp.transform.gameObject.SetActive(false);
                                PopUpText.transform.gameObject.SetActive(false);
                                PopUpScroll.transform.gameObject.SetActive(false);
                                Link.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateImgOptionQuiz(2, tempInt);
                                ImgOptionCanvas.transform.gameObject.SetActive(true);
                                CH3.transform.gameObject.SetActive(false);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH2_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.ImgOptionClear = false;
                                ImgOptionCanvas.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                CH1_nomal.transform.gameObject.SetActive(false);
                                CH2_nomal.transform.gameObject.SetActive(false);
                                CH3.transform.gameObject.SetActive(true);
                                CHS.SetTrigger("teachAnim");
                                count++;                        
                            }
                            break;
                        case 7:
                            if (linkQuizManager.ImgOptionClear == false)
                            {
                                str.text = string.Empty;
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                Link.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateImgOptionQuiz(2, tempInt);
                                ImgOptionCanvas.transform.gameObject.SetActive(true);
                                CH3.transform.gameObject.SetActive(false);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH2_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.ImgOptionClear = false;
                                ImgOptionCanvas.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                CH1_nomal.transform.gameObject.SetActive(false);
                                CH2_nomal.transform.gameObject.SetActive(false);
                                CH3.transform.gameObject.SetActive(true);
                                CHS.SetTrigger("teachAnim");
                                count++;
                            }
                            break;
                        case 8:
                            AppManage.Instance.EndStage(currentStage);
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 0:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 1:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.visitjeju.net/kr/detail/view?contentsid=CONT_000000000500364");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "소남머리";
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            if(AppManage.Instance.Gender==0)
                            {
                                CH2_nomal.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.transform.gameObject.SetActive(false);
                            }
                            CH3.transform.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 2:
                            if (linkQuizManager.OptionClear == false)
                            {
                                PopUp.transform.gameObject.SetActive(false);
                                PopUpText.text = string.Empty;
                                PopUpText.transform.gameObject.SetActive(false);
                                PopUpScroll.transform.gameObject.SetActive(false);
                                Link.transform.gameObject.SetActive(false);
                                CH3.transform.gameObject.SetActive(false);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH2_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.transform.gameObject.SetActive(true);
                                    CHS.SetTrigger("CH1Anim");
                                }
                                linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                            }
                            else
                            {
                                tempInt++;
                                AppManage.Instance.isComplite = true;
                                linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                linkQuizManager.OptionClear = false;
                            }
                            if (tempInt == 2)
                            {
                                AppManage.Instance.EndStage(currentStage);
                            }
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 5:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 3:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 4:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 5:
                            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                            GameObject.Find("UIManager").SendMessage("CaptureOn");
                            GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, true);
                            AppManage.Instance.isComplite = true;
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch(currentStage)
            {
                case 1:
                    switch(count)
                    {
                        case 2:
                        case 3:
                        case 15:
                        case 26:
                        case 35:
                        case 39:            
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 4:
                        case 6:
                        case 8:
                        case 10:
                        case 13:
                        case 14:
                        case 16:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 24:
                        case 25:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                        case 33:
                        case 34:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 11:
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch(count)
                    {
                        case 2:
                        case 12:
                        case 14:
                        case 20:
                        case 21:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 13:
                        case 15:
                        case 16:
                        case 17:
                        case 19:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 31:
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 6:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 1:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 2:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 1:
                        case 2:
                        case 3:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 4:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
            }
            switch(currentStage)
            {
                case 1:
                    switch(count)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 37:
                        case 38:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 11:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            break;
                        case 31:
                        case 36:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch(count)
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 10:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 6:
                        case 7:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 1:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 2:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 5:
                    switch(count)
                    {
                        case 1:
                        case 2:
                        case 3:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]] == "*")
            {
                if (AppManage.Instance.Gender == 0)
                {
                    CHS.SetTrigger("CH2Anim");
                }
                else
                {
                    CHS.SetTrigger("CH1Anim");
                }
            }
            else
            {
                if (currentStage == 1)
                {
                    if (count != 31)
                    {
                        CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if(currentStage==2)
                {
                    if(count!=10)
                    {
                        CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if(currentStage==3)
                {
                    if(count!=6&&count!=7)
                    {
                        CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if(currentStage==4)
                {
                    if(count!=2)
                    {
                        CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else
                {
                    CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                }
            }
            AppManage.Instance.isComplite = false;
            if(currentStage==1)
            {
                if(count==11)
                {
                    AppManage.Instance.isComplite = true;
                }
            }
            else if(currentStage==2)
            {
                if(count==31)
                {
                    AppManage.Instance.isComplite = true;
                }
            }
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }

    public void BeforeDown()
    {
        if (AppManage.Instance.isComplite)
        {
            if (count > 0)
            {
                count--;
            }
            else
            {
                SceneManager.LoadScene("SelectMap");
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            BackGround.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(false);
                            break;
                        case 1:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 2:
                            Link.transform.gameObject.SetActive(true);
                            break;
                        case 3:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.sprite = Resources.Load<Sprite>("Popup_Png");
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            break;
                        case 4:
                        case 6:
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            break;
                        case 5:
                        case 7:
                        case 9:
                        case 12:
                        case 23:
                        case 27:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 8:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            BackGround.sprite = Resources.Load<Sprite>("이중섭거리BG");
                            break;
                        case 10:
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            break;
                        case 11:
                            PopUp.sprite = Resources.Load<Sprite>("이중섭소의말");
                            PopUp.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            break;
                        case 14:
                            Masterpiece.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 15:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("이중섭_황소");
                            Masterpiece.transform.gameObject.SetActive(true);
                            tempInt = 0;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            break;
                        case 16:
                            tempInt = 0;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            break;
                        case 17:
                            video.time = 0.0f;
                            video.Stop();
                            break;
                        case 18:
                            video.Play();
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 22:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            BackGround.sprite = Resources.Load<Sprite>("이중섭생가BG");
                            break;
                        case 25:
                            Masterpiece.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 26:
                            Masterpiece.sprite = Resources.Load<Sprite>("familyAndArtist");
                            Masterpiece.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            break;
                        case 30:
                            tempInt = 1;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 31:
                            tempInt = 1;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            CH1_nomal.transform.gameObject.SetActive(false);
                            CH2_nomal.transform.gameObject.SetActive(false);
                            CH3.transform.gameObject.SetActive(true);
                            break;
                        case 34:
                            Masterpiece.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            break;
                        case 35:
                            BackGround.sprite = Resources.Load<Sprite>("생가내부");
                            main.transform.gameObject.SetActive(true);
                            canvas.worldCamera = main;
                            puzzleCamera.transform.gameObject.SetActive(false);
                            Puzzle.pazzle_control.step = PazzleControl.STEP.NONE;
                            Puzzle.step = GameControl.STEP.PLAY;
                            Puzzle.OnRetryButtonPush();
                            Puzzle.pazzle_control.transform.gameObject.SetActive(false);
                            GameCanvas.GetChild(1).gameObject.SetActive(true);
                            GameCanvas.GetChild(2).gameObject.SetActive(false);
                            GameCanvas.transform.gameObject.SetActive(false);
                            Masterpiece.sprite = Resources.Load<Sprite>("family6");
                            Masterpiece.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            break;
                        case 38:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            BackGround.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(false);
                            break;
                        case 1:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            break;
                        case 2:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            break;
                        case 8:
                            Masterpiece.transform.gameObject.SetActive(false);
                            break;
                        case 9:
                            tempInt = 0;
                            linkQuizManager.ImgOptionClear = false;
                            ImgOptionCanvas.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(false);
                            break;
                        case 10:
                            tempInt = 0;
                            linkQuizManager.ImgOptionClear = false;
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("정미진작가 게와 아이들");
                            NPC.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            break;
                        case 11:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 12:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("crabAndChild");
                            Masterpiece.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            break;
                        case 14:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("twoChildren");
                            Masterpiece.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            break;
                        case 13:
                        case 19:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 16:
                            tempInt = 1;
                            linkQuizManager.ImgLinkClear = false;
                            ImgLinkCanvas.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 17:
                            tempInt = 1;
                            linkQuizManager.ImgLinkClear = false;
                            ImgLinkCanvas.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 18:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 20:
                            tempInt = 2;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("crabAndChild");
                            break;
                        case 21:
                            tempInt = 2;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.transform.gameObject.SetActive(true);
                            Masterpiece.sprite = Resources.Load<Sprite>("twoChildren");
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            break;
                        case 22:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPC.SetTrigger("leeJuongSub_Idle");
                            break;
                        case 25:
                            NPC.SetTrigger("leeJuongSub_Smile");
                            break;
                        case 30:
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            ArtSlide.transform.gameObject.SetActive(false);
                            break;
                        case 31:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            ArtSlide.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 0:
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            break;
                        case 1:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            break;
                        case 5:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            tempInt = 0;
                            linkQuizManager.ImgOptionClear = false;
                            ImgOptionCanvas.transform.gameObject.SetActive(false);
                            break;
                        case 6:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            tempInt = 0;
                            linkQuizManager.ImgOptionClear = false;
                            ImgOptionCanvas.transform.gameObject.SetActive(false);
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 0:
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 1:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            Link.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            tempInt = 0;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCScroll.transform.gameObject.SetActive(false);
                            break;
                        case 3:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Masterpiece.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 4:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            Masterpiece.transform.gameObject.SetActive(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 15:
                        case 26:
                        case 35:
                        case 39:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 4:
                        case 6:
                        case 8:
                        case 10:
                        case 13:
                        case 14:
                        case 16:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 24:
                        case 25:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                        case 33:
                        case 34:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 11:
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 2:
                        case 12:
                        case 14:
                        case 20:
                        case 21:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 13:
                        case 15:
                        case 16:
                        case 17:
                        case 19:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 31:
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 6:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 1:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 1:
                        case 2:
                        case 3:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 4:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Text_XML_Reader.Instance.scenario[currentStage].text[Text_XML_Reader.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 11:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(false);
                            break;
                        case 31:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 32:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 10:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 1:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 1:
                        case 2:
                        case 3:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]] == "*")
            {
                if (AppManage.Instance.Gender == 0)
                {
                    CHS.SetTrigger("CH2Anim");
                }
                else
                {
                    CHS.SetTrigger("CH1Anim");
                }
            }
            else
            {
                if (currentStage == 1)
                {
                    CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                }

                else
                {
                    CHS.SetTrigger(Text_XML_Reader.Instance.scenario[currentStage].anim[Text_XML_Reader.Instance.scenario[currentStage].Num[count]]);
                }
            }
            AppManage.Instance.isComplite= false;
            if (currentStage == 1)
            {
                if (count == 11)
                {
                    AppManage.Instance.isComplite = true;
                }
            }
            else if (currentStage == 2)
            {
                if (count == 31)
                {
                    AppManage.Instance.isComplite = true;
                }
            }
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
        
    }
    public void ExitCapture()
    {
        if (AppManage.Instance.isComplite)
        {
            if (currentStage == 5)
            {
                AppManage.Instance.EndStage(currentStage);
            }
        }
    }
}
