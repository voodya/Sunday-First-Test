using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Viewer : MonoBehaviour
{
    [SerializeField] private RawImage _targetImage;
    [SerializeField] private Button _return;
    [SerializeField] private Texture2D _errorImg; 

    public static Action<string> OnSetImage;

    private void Awake()
    {
        OnSetImage += SetImage;
        _return.onClick.AddListener(Return);
    }

    private void OnDestroy()
    {
        OnSetImage -= SetImage;
    }

    private void Return()
    {
        SceneController.instance.Return();
    }

    private async void SetImage(string obj)
    {
       _targetImage.texture = await Downloader.DownloadImage(obj);
       
       if (_targetImage.texture == null)
           _targetImage.texture = _errorImg;
    }
}
