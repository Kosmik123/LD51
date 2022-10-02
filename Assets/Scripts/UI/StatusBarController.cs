using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class StatusBarController : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField, Required]
    private Slider slider;
    [SerializeField, Required]
    private Image fillImage;

    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI valueLabel;
    [SerializeField]
    private TextMeshProUGUI maxValueLabel;

    [Header("Settings")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool useGradient;

    [SerializeField, ShowIf(nameof(useGradient))]
    private Gradient gradient;
    [SerializeField, HideIf(nameof(useGradient))]
    private Color color = Color.white;
    
    public Color Color
    {
        get => useGradient ? gradient.Evaluate(currentValue) : color;
        set
        {
            color = value;
            fillImage.color = color;
        }
    }

    [SerializeField]
    private bool isValueInt;

    [SerializeField]
    private AnimationCurve progressCurve;

    [Header("States")]
    private float currentValue;
    private float previousValue;

    public float Value
    {
        get => currentValue;
        set
        {
            StopAllCoroutines();
            previousValue = currentValue;
            currentValue = value;
            StartCoroutine(nameof(AnimateSliderCo));
        }
    }

    [SerializeField]
    private float maxValue;
    public float MaxValue 
    {
        get => maxValue;
        internal set
        {
            maxValue = value;
            oneOverMaxValue = 1 / maxValue;
        }
    }
    private float oneOverMaxValue;

    private const string intFormat = "F0";
    private const string floatFormat = "";
    private string NumberFormat => isValueInt ? intFormat : floatFormat;

    private readonly WaitForEndOfFrame wait = new WaitForEndOfFrame();
    private IEnumerator AnimateSliderCo()
    {
        float progress = 0;
        while(progress < 1)
        {
            progress += Time.deltaTime * speed;
            float targetValue = Mathf.Lerp(previousValue, currentValue, progress);
            slider.value = progressCurve.Evaluate(targetValue * oneOverMaxValue);
            RefreshLabels(targetValue);
            yield return wait;
        }
        slider.value = progressCurve.Evaluate(currentValue * oneOverMaxValue);
    }


    private void RefreshLabels(float value)
    {
        if(maxValueLabel != null)
            maxValueLabel.text = maxValue.ToString(NumberFormat);
        if (valueLabel != null)
            valueLabel.text = Mathf.Min(value, maxValue).ToString(NumberFormat);
    }

    private void OnValidate()
    {
        Color = Color;
    }

}
