using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
	public static SceneController instance = null;
	public Action<string> OnOpenScene;
	public Action OnNextScene;

	private string _afterLoadScene;
	public List<string> _sceneHistory;
	private string _currentScene;

    private void Awake()
    {
		if(instance == null) 
			instance = this;
		else
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

    private void Start()
	{
		_sceneHistory = new List<string>();
		OnOpenScene += OpenSceneNew;
		OnNextScene += OpenNext;
		_currentScene = "Menu";
		AddSceneHistory("Menu");


		Screen.orientation = UnityEngine.ScreenOrientation.Portrait;

		Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;


	}

    private void OpenNext()
    {
		OpenScene(_afterLoadScene);
    }

    private void OpenScene(string name)
    {
		_currentScene = name;

		if(_currentScene == "Menu" || _currentScene == "Galery_native_grid" || _currentScene == "Galery_custom_grid" || _currentScene == "FakeLoadScene")
        {
			Screen.orientation = UnityEngine.ScreenOrientation.Portrait;

			Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
			Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
		}
		else
        {
			Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;

			Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
			Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
		}


		if (name != "FakeLoadScene") AddSceneHistory(name);
		SceneManager.LoadScene(name);
		

    }

	private void OpenSceneNew(string name)
    {
		_afterLoadScene = name;
		OpenScene("FakeLoadScene");
	}

	private void AddSceneHistory(string name)
    {
		if(!_sceneHistory.Contains(name))
		_sceneHistory.Add(name);
	}


	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Return();
		}
		
	}

	public void Return()
    {
		Debug.Log("Return Work");
		if (_currentScene == "FakeLoadScene") return;
		if(_currentScene == "Menu") Application.Quit();
		else
        {
			string targetScene = _sceneHistory[_sceneHistory.Count - 2];
			_sceneHistory.RemoveAt(_sceneHistory.Count - 1);
			_afterLoadScene = targetScene;
			SceneManager.LoadScene("FakeLoadScene");
		}
    }
}


