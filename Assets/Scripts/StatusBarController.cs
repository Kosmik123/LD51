using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image fillImage;
    
    [Header("Settings")]
    [SerializeField]
    private float speed;

    private Color color = Color.white;
    public Color Color
    {
        get => color;
        set
        {
            color = value;
            fillImage.color = color;
        }
    }

    [Header("States")]
    private float targetValue;
    private float previousValue;

    public float Value
    {
        get => slider.value;
        set
        {
            StopAllCoroutines();
            previousValue = slider.value;
            targetValue = value;
            StartCoroutine(nameof(AnimateSliderCo));
        }
    }


    private readonly WaitForEndOfFrame wait = new WaitForEndOfFrame();
    private IEnumerator AnimateSliderCo()
    {
        float progress = 0;
        while(progress < 1)
        {
            progress += Time.deltaTime * speed;
            slider.value = Mathf.Lerp(previousValue, targetValue, progress);
            yield return wait;
        }
        slider.value = targetValue;
    }

    private void OnValidate()
    {
        Color = Color;
    }

}
