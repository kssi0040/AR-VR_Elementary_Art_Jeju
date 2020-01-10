using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UImanager : MonoBehaviour
{
    public RectTransform PaintCanvas;
    public RectTransform Capture;
    public RectTransform Contents;
    public TextManager manager;
    public Transform WebCam;
	// Use this for initialization
	void Start ()
    {
        if (manager.currentStage == 5)
        {
            PaintCanvas.transform.gameObject.SetActive(false);
            Contents.transform.gameObject.SetActive(true);
            WebCam.transform.gameObject.SetActive(false);
            Capture.transform.gameObject.SetActive(false);
        }
        else
        {
            Contents.transform.gameObject.SetActive(true);
            WebCam.transform.gameObject.SetActive(false);
            Capture.transform.gameObject.SetActive(false);
        }
	}

    public void CaptureOn()
    {
        if (manager.currentStage == 5)
        {
            PaintCanvas.transform.gameObject.SetActive(true);
            Contents.transform.gameObject.SetActive(false);
            WebCam.transform.gameObject.SetActive(false);
            Capture.transform.gameObject.SetActive(false);
            GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, true);
        }
        else
        {
            Capture.transform.gameObject.SetActive(false);
            Contents.transform.gameObject.SetActive(false);
            WebCam.transform.gameObject.SetActive(false);
            GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, true);
        }
    }

    public void CaptureOff()
    {
        if (manager.currentStage == 5)
        {
            PaintCanvas.transform.gameObject.SetActive(false);
            Contents.transform.gameObject.SetActive(true);
            Capture.transform.gameObject.SetActive(false);
            WebCam.transform.gameObject.SetActive(false);
            GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, false);
        }
        else
        {
            Contents.transform.gameObject.SetActive(true);
            Capture.transform.gameObject.SetActive(false);
            WebCam.transform.gameObject.SetActive(false);
            GameObject.Find("CaptureManager").GetComponent<TakeCapture>().TakeShotWithKids(GameObject.Find("CaptureManager").GetComponent<TakeCapture>().Kids, false);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
