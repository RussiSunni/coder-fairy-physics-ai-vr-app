using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;
using Facebook.WitAi.TTS.Samples;
using TMPro;
using UnityEngine.Networking;
using System.Text;

public class AskOpenAIController : MonoBehaviour
{
    [SerializeField] private TTSSpeakerInput _speaker;
   // public Text questionText;
    public TMP_Text questionText;
    public TMP_Text answerText;
    public TMP_Text buttonTest;

    private void Start()
    { }   
        
    public IEnumerator CallOpenAI(string url, string questionText)
    {
        // Get the API key.
        // Need to implement this still.
        //var keyPath = Path.Combine(Application.streamingAssetsPath, "apiKey.txt");
        //if (File.Exists(keyPath) == false)
        //{
        //    Debug.Log("Api key missing: " + keyPath);
        //}
        //var apiKey = File.ReadAllText(keyPath);        
        //var openAiKey = apiKey;

        var request = new Request();

        // Hard coded for now.
        request.model = "text-davinci-003";       
        request.prompt = questionText;
        request.max_tokens = 10;
        request.temperature = 0.7f;

        // Below params not working yet.
        //request.top_p = 1;
        //request.n = 1;
        //request.stream = false;
        //request.logprobs = null; 

        string json = JsonUtility.ToJson(request);

        var req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Authorization", "Bearer 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");

        //Send the request then wait here until it returns
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);         
            Response responseObject = JsonUtility.FromJson<Response>(req.downloadHandler.text);

            // Print answer to screen.
            answerText.text = responseObject.choices[0].text;
            // Speak Answer.
            _speaker.SayPhrase(responseObject.choices[0].text);
        }
    }

    public void AskQuestion()
    {
        // Receive question.         
        StartCoroutine(CallOpenAI("https://api.openai.com/v1/completions", questionText.text));
    }

    // Creating a class to send the POST request to the OpenAI API.
    [System.Serializable]
    public class Request
    {
        public string model;
        public string prompt;
        public int max_tokens;
        public float temperature;

        // Below properties not working for the API call yet.
        //public int top_p;
        //public int n;
        //public bool stream;
        //public string logprobs;
    }

    // In order to get the response object from the JSON. --------

    [System.Serializable]
    public class Response
    {
        public string id;
        public string objectName;
        public int created;
        public string model;
        public Choice[] choices;
        public Usage usage;        
    }

    [System.Serializable]
    public class Choice
    {
        public string text;
        public int index;
        public string logprobs;
        public string finish_reason;          
    }

    [System.Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }
}