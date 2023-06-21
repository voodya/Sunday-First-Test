using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Downloader : MonoBehaviour
{
    private static string _downloadPath = $"{Application.persistentDataPath}/Images/";

    public static async Task<Texture2D> DownloadImage(string Url)
    {
        string filePath = $"{_downloadPath}{Path.GetFileName(Url)}";
        if (File.Exists(filePath))
        {
            Texture2D temp = new Texture2D(1, 1);
            temp.LoadImage(File.ReadAllBytes(filePath));
            return temp;
        }
        UnityWebRequest request = UnityWebRequest.Get(Url);
        request.SendWebRequest();

        while (!request.isDone || !Application.isPlaying)
        {
            await Task.Yield();
        }

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError ||
            request.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError($"{request.error} \n{request.downloadHandler.error}");
            return null;
        }
        else
        {
            if (!Directory.Exists(_downloadPath)) Directory.CreateDirectory(_downloadPath);
            File.WriteAllBytes(filePath, request.downloadHandler.data);
            Texture2D temp = new Texture2D(1, 1);
            temp.LoadImage(request.downloadHandler.data);
            return temp;
        }
    }
}

