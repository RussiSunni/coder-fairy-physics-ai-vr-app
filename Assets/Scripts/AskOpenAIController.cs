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
using Newtonsoft.Json.Linq;

public class AskOpenAIController : MonoBehaviour
{
    [SerializeField] private TTSSpeakerInput _speaker;
   // public Text questionText;
    public TMP_Text questionText;
    public TMP_Text answerText;
    public TMP_Text buttonTest;

    private void Start()
    {
    }

    private static string callOpenAI(int tokens, string input, string engine, double temperature, int topP, int frequencyPenalty, int presencePenalty)
    {
        // Get the API key.
        //var keyPath = Path.Combine(Application.streamingAssetsPath, "apiKey.txt");
        //if (File.Exists(keyPath) == false)
        //{
        //    Debug.Log("Api key missing: " + keyPath);
        //}
        //var apiKey = File.ReadAllText(keyPath);        
        //var openAiKey = apiKey;

        // Need to change.
        var openAiKey = "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";

        // Call the API.
        var apiCall = "https://api.openai.com/v1/engines/" + engine + "/completions";
        try
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), apiCall))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + openAiKey);
                    request.Content = new StringContent("{\n  \"prompt\": \"" + input + "\",\n  \"temperature\": " +
                                                        temperature.ToString(CultureInfo.InvariantCulture) + ",\n  \"max_tokens\": " + tokens + ",\n  \"top_p\": " + topP +
                                                        ",\n  \"frequency_penalty\": " + frequencyPenalty + ",\n  \"presence_penalty\": " + presencePenalty + "\n}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).Result;
                    var json = response.Content.ReadAsStringAsync().Result;

                    Debug.Log(json);

                    Response responseObject = JsonUtility.FromJson<Response>(json);
                  
                    Debug.Log(responseObject.choices[0].text);

                    return responseObject.choices[0].text;                
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public void AskQuestion()
    {     
        // Receive question.
        var answer = callOpenAI(250, questionText.text, "text-davinci-002", 0.7, 1, 0, 0);

        // Print answer to screen.
        answerText.text = answer;

        // Speak Answer.
        _speaker.SayPhrase(answer);
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

// attempting to call method "system.linq.expressions.interpreter.lightlambda"


