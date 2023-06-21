using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBrain : MonoBehaviour
{
    [SerializeField] private ScrollRect _container;
    [SerializeField] private ScrollElement _pref;
    [SerializeField] private string _urlBody = "http://data.ikppbb.com/test-task-unity-data/pics/";

    [SerializeField] private int _imageCount = 66; // в данном случае ок, но лучше иметь доступ к списку
                                                   // изображений на сервере и/или предварительно загружаемому Json'у, иначе выходит хардкод.
                                                   // Можно еще проверять по ответу сервера (при 404 заканчиваем), но актуально сугубо для последственного нейминга файлов
    [SerializeField] private RectTransform _targetRect;
    [SerializeField] private List<ScrollPeir> _elems;
    [SerializeField] private float _spacing;
    [SerializeField] private float _height;
    [SerializeField] private float _widh;
    [SerializeField] private float _top;

    [SerializeField] private List<List<string>> _data;

    private float Width { get => Screen.width * _widh; }
    private float Height { get => Screen.height * _height; }
    private float Spacing { get => Screen.height * _spacing; }
    private float TopSpace { get => Screen.height * _top; }


    private float containerheight;
    private int lastLoadedData;
    private int lastMovedElem;
    private int TargetPosition = 0;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _container.onValueChanged.AddListener(OnValueChanget);
        SetCrollState();
    }

    private void SetCrollState()
    {
        int objCount = 0;
        if(_imageCount % 2 == 0)
        {
            objCount = _imageCount / 2;
        }
        else
            objCount = _imageCount / 2 + 1;


        for (int i = 0; i < _elems.Count; i++)
        {
            _elems[i].rectTransform.sizeDelta = new Vector2(Width, Height);
            _elems[i].rectTransform.anchoredPosition = new(0f, (-Height - Spacing) * i - TopSpace);
        }
        containerheight = (objCount * (Height + Spacing)) + TopSpace * 2;
        _targetRect.sizeDelta = new Vector2(_targetRect.sizeDelta.x, containerheight);

        _data = new List<List<string>>();
        int counter = 0;
        for (int i = 0; i < objCount; i++)
        {
            List<string> temp = new List<string>();
            for (int j = 0; j < 2; j++)
            {
                if(counter < _imageCount)
                {
                    temp.Add($"{_urlBody}{counter + 1}.jpg");
                    counter++;
                }
            }
            _data.Add(temp);
        }

        for (int i = 0; i < _elems.Count; i++)
        {
            _elems[i].Init(_data[i]);
            lastLoadedData = i;
        }
        lastMovedElem = 0;
    }

    private void OnValueChanget(Vector2 delta)
    {
        int numberOfData = (int)_targetRect.anchoredPosition.y / (int)(Height + Spacing);

        if(TargetPosition + 4 < numberOfData && lastLoadedData < _data.Count-1)
        {
            TargetPosition++;
            lastLoadedData++;
            _elems[lastMovedElem].rectTransform.anchoredPosition = new (0f, (-Height - Spacing) * lastLoadedData - TopSpace);
            _elems[lastMovedElem].Init(_data[lastLoadedData]);
            if(lastMovedElem == _elems.Count - 1)
            {
                lastMovedElem = 0;
            }
            else
                lastMovedElem++;
        }
        else if(TargetPosition + 4 > numberOfData && TargetPosition > 0)
        {
            TargetPosition--;
            lastLoadedData--;
            if (lastMovedElem == 0)
            {
                lastMovedElem = _elems.Count - 1;
            }
            else
                lastMovedElem--;
            _elems[lastMovedElem].rectTransform.anchoredPosition = new(0f, (-Height - Spacing) * (lastLoadedData - (_elems.Count-1)) - TopSpace);
            _elems[lastMovedElem].Init(_data[lastLoadedData - (_elems.Count - 1)]);

        }

    }
}
