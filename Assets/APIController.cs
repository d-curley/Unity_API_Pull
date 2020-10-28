using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class APIController : MonoBehaviour
{
    public string baseURL = "https://api.particle.io/v1/devices/20001e001447393035313136/Analog_Value1?access_token=f68d090ee21d7da181565c0fde00cf0f2e8594c5";
    public InputField inputURL;
    public Text text;
    float lastTime=0f;
    float loopTime;

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
            Debug.Log(inputURL.text);
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
