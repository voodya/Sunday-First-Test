using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPeir : MonoBehaviour
{
    [SerializeField] private List<ScrollElement> children;
    [SerializeField] public RectTransform rectTransform;

    public void Init(List<string> elemsData)
    {
        if(elemsData.Count == 2)
        {
            for (int i = 0; i < elemsData.Count; i++)
            {
                children[i].Build(elemsData[i]);
            }
        }
        else
        {
            children[0].Build(elemsData[0]);
            children[1].gameObject.SetActive(false);
        }
    }
}
