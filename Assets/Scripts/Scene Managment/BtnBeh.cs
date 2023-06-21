using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnBeh : MonoBehaviour
{
    [SerializeField] private string _scenTypeToOpen;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            SceneController.instance.OnOpenScene?.Invoke(_scenTypeToOpen);
        });
    }
}
