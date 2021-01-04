using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class APIController : MonoBehaviour
{
    string baseURL = "https://api.particle.io/v1/devices/20001e001447393035313136/Analog_Value1?access_token=f5fc512817a12f0d5f193095f8474ce42fcb3c8e";

    public Text text;

    void Start()
    {
        text.text = "waiting for web request";
        StartCoroutine(GetValue());
    }

    // Update is called once per frame
    IEnumerator GetValue()
    {
        while (true)
        {
            //Initialize Web request
            UnityWebRequest InfoRequest = UnityWebRequest.Get(baseURL);
            yield return InfoRequest.SendWebRequest(); 

            //Break coroutine if error is detected, print that error
            if (InfoRequest.isNetworkError || InfoRequest.isHttpError)
            {
                Debug.Log(InfoRequest.error);
                yield break;
            }

            //Parse the JSON Node and print data to screen
            JSONNode Info = JSON.Parse(InfoRequest.downloadHandler.text);
            text.text=Info["result"];

            //Slight delay to prevent coroutine from tripping on itself
            yield return new WaitForSeconds(.33f);

            yield return null;
        }
        
    }
}
