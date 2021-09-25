using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class StarCount : MonoBehaviour
{
    public Text count;

    void Start()
    {
        GameController.Instance.Stars.Subscribe((value) =>
        {
            count.text = value.ToString();
        }).AddTo(this);
    }
}
