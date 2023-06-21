using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ImageHendler : MonoBehaviour
{
    public static ImageHendler instance = null;

	public Action<string> OnSetUrl;
	private string _imageUrl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
	{
		OnSetUrl += SetUrl;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "Viewer" && !string.IsNullOrEmpty(_imageUrl))
        {
			Viewer.OnSetImage?.Invoke(_imageUrl);
		}
    }

    private void SetUrl(string obj)
    {
        _imageUrl = obj;
    }

	
}
