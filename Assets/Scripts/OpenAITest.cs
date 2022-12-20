using Newtonsoft.Json;
using OpenAI_API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OpenAITest : MonoBehaviour
{
    public Text questionText;
    public Text answerText;
    private void Start()
    {
        // var task = StartAsync();

      //  var question = "What is your name?";

        //call the open ai
        //var answer = callOpenAI(250, question, "text-davinci-002", 0.7, 1, 0, 0);       

        //print(answer);
    }

    //async Task StartAsync()
    //{       
    //    var keyPath = Path.Combine(Application.streamingAssetsPath, "apiKey.txt");
    //    if (File.Exists(keyPath) == false)
    //    {
    //        Debug.Log("Api key missing: " + keyPath);
    //    }

    //    //Debug.Log("Load apiKey: " + keyPath);

    //    // Debug.Log("Sent to OpenAI: 'To be, or not to be:'");

    //    var apiKey = File.ReadAllText(keyPath);

    //    try
    //    {
    //        var api = new OpenAI_API.OpenAIAPI(apiKey, Engine.Davinci);         
    //        var result = await api.Completions.CreateCompletionAsync("One Two Three One Two", temperature: 0.1);            
    //        Debug.Log("Result: " + result.ToString());
    //    }
    //    catch(System.Exception e)
    //    {
    //        Debug.LogError(e.Message);
    //    }
    //}    

    private static string callOpenAI(int tokens, string input, string engine, double temperature, int topP, int frequencyPenalty, int presencePenalty)
    {
        var keyPath = Path.Combine(Application.streamingAssetsPath, "apiKey.txt");
        if (File.Exists(keyPath) == false)
        {
            Debug.Log("Api key missing: " + keyPath);
        }

        var apiKey = File.ReadAllText(keyPath);

        var openAiKey = apiKey;
        //var openAiKey = "API_KEY";
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

                    dynamic dynObj = JsonConvert.DeserializeObject(json);

                    if (dynObj != null)
                    {
                        return dynObj.choices[0].text.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;

    }

    public void AskQuestion()
    {
        var answer = callOpenAI(250, questionText.text, "text-davinci-002", 0.7, 1, 0, 0);
        Console.WriteLine(answer);

        answerText.text = answer;
        //print(answer);
    }
}
