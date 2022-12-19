using OpenAI_API;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class OpenAITest : MonoBehaviour
{
    private void Start()
    {
        var task = StartAsync();
    }

    async Task StartAsync()
    {       
        var keyPath = Path.Combine(Application.streamingAssetsPath, "apiKey.txt");
        if (File.Exists(keyPath) == false)
        {
            Debug.Log("Api key missing: " + keyPath);
        }

        //Debug.Log("Load apiKey: " + keyPath);

        Debug.Log("Sent to OpenAI: 'To be, or not to be:'");

        var apiKey = File.ReadAllText(keyPath);

        try
        {
            var api = new OpenAI_API.OpenAIAPI(apiKey, Engine.Davinci);

            //var result = await api.Completions.CreateCompletionAsync("One Two Three One Two", temperature: 0.1);
            // var result = await api.Completions.CreateCompletionAsync("A B C, A B", temperature: 0.1);
            // var result = await api.Completions.CreateCompletionAsync("Lorem ipsum dolor sit amet,", temperature: 0.1);
            var result = await api.Completions.CreateCompletionAsync("To be, or not to be:", temperature: 0.1);

            //var result = await api.Search.GetBestMatchAsync("Washington DC", "Canada", "China", "USA", "Spain");

            Debug.Log("Result: " + result.ToString());
        }
        catch(System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }    
}
