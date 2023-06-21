using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FakeLoadSceneBrain : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _loadTExt;
    [SerializeField] private float _timeInSec;

    private void Start()
    {
        StartCoroutine(FakeLoad());
    }

    private IEnumerator FakeLoad()
    {
        float value = 0;
        float _h = 1 / (_timeInSec*10);
        while (value < 1)
        {
            value += _h;
            _slider.value = value;
            _loadTExt.text = $"Загрузка {(int)(value*100)}%";
            yield return new WaitForSeconds(0.1f);
        }
        SceneController.instance.OnNextScene?.Invoke();
    }
}
