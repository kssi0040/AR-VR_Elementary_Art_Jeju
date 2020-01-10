using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public string text;
    public RawImage TextBox;
    public Text str;
    public Transform CH1_nomal;
    public Transform CH2_nomal;
    public Transform CH3;
    public Animator Character;
    public int Stage;
    public int count;
    // Use this for initialization
    void Start()
    {
        count = 0;
        CH1_nomal.transform.gameObject.SetActive(false);
        CH2_nomal.transform.gameObject.SetActive(false);
        CH3.transform.gameObject.SetActive(false);
        switch (Stage)
        {
            case 0:
            case 2:
            case 6:
                CH3.transform.gameObject.SetActive(true);
                break;
            case 1:
                if (AppManage.Instance.Gender == 0)
                {
                    CH2_nomal.transform.gameObject.SetActive(true);
                    Character.SetTrigger("CH2Anim");
                }
                else
                {
                    CH1_nomal.transform.gameObject.SetActive(true);
                    Character.SetTrigger("CH1Anim");
                }
                break;
            default:
                break;
        }
        switch (Stage)
        {
            case 0:
            case 6:
                text = Text_XML_Reader.Instance.scenario[Stage].text[count];
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
            default:
                ChangeStr(Stage);
                break;
        }
        AppManage.Instance.isComplite = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeStr(int _stage)
    {
        text = Text_XML_Reader.Instance.scenario[_stage].text[Text_XML_Reader.Instance.scenario[_stage].text.Count - 1];

        if (text.Contains("###"))
        {
            string temp = text.Replace("###", AppManage.Instance.Name);
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
        }
        else
        {
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
        }
    }

    public void NextScript()
    {
        if (AppManage.Instance.isComplite)
        {
            switch (Stage)
            {
                case 0:
                    switch (count)
                    {
                        case 1:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH2_nomal.transform.gameObject.SetActive(true);
                                CH3.transform.gameObject.SetActive(false);
                                Character.SetTrigger("CH2Anim");
                            }
                            else
                            {
                                CH1_nomal.transform.gameObject.SetActive(true);
                                CH3.transform.gameObject.SetActive(false);
                                Character.SetTrigger("CH1Anim");
                            }
                            count++;
                            break;
                        case 2:
                            AppManage.Instance.isComplite = false;
                            SceneManager.LoadScene("SelectMap");
                            break;
                        default:
                            CH1_nomal.transform.gameObject.SetActive(false);
                            CH2_nomal.transform.gameObject.SetActive(false);
                            CH3.transform.gameObject.SetActive(true);
                            Character.SetTrigger("teachAnim");
                            count++;
                            break;
                    }
                    break;
                case 6:
                    switch(count)
                    {
                        case 1:
                            AppManage.Instance.isComplite = false;
                            TextBox.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            AppManage.Instance.isExit = true;
                            count++;
                            break;
                        default:
                            CH1_nomal.transform.gameObject.SetActive(false);
                            CH2_nomal.transform.gameObject.SetActive(false);
                            CH3.transform.gameObject.SetActive(true);
                            Character.SetTrigger("teachAnim");
                            count++;
                            break;
                    }
                    break;
                default:
                    break;
            }
            text = Text_XML_Reader.Instance.scenario[Stage].text[Text_XML_Reader.Instance.scenario[Stage].Num[count]];
            if (text.Contains("###"))
            {
                string temp = text.Replace("###", AppManage.Instance.Name);
                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
            }
            else
            {
                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
            }
            AppManage.Instance.isComplite = false;
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }
}
