using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeScrollController : MonoBehaviour
{
    [SerializeField] private ScrollRect _container;
    [SerializeField] private NativeScrollElement _pref;
    [SerializeField] private string _urlBody = "http://data.ikppbb.com/test-task-unity-data/pics/";
    [SerializeField] private int _imageCount = 66;
    [SerializeField] private List<NativeScrollElement> _preloaded;

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetData();
    }

    private void SetData()
    {
        for (int i = 0; i < _imageCount; i++)
        {
            string targetUrl = $"{_urlBody}{i + 1}.jpg";
            if (i < _preloaded.Count)
            {
                _preloaded[i].Build(targetUrl);
            }
            else
            {
                var temp = Instantiate(_pref, _container.content);
                temp.Build(targetUrl);
                _preloaded.Add(temp);
            }
        }
    }
}
