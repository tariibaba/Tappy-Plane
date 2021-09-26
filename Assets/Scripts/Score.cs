using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Score : MonoBehaviour
{
    private int score;
    public Sprite[] digitImageSprites;
    public Image digitImage;
    private List<Image> digitImages;

    private void Start()
    {
        digitImages = new List<Image>();
        digitImages.Add(digitImage);
        GameController.Instance.Score.Subscribe((value) =>
        {
            score = value;
            UpdateScore();
        }).AddTo(this);
    }

    private void UpdateScore()
    {
        var scoreStr = score.ToString();
        var scoreDigitLength = scoreStr.Length;
        for (int i = 0; i < scoreDigitLength; i++)
        {
            if (i >= digitImages.Count)
            {
                var newImage = Instantiate(digitImage, digitImage.transform.parent);
                digitImages.Add(newImage);
            }
            var digit = int.Parse(scoreStr[i].ToString());
            digitImages[i].sprite = digitImageSprites[digit];
        }
    }
}
