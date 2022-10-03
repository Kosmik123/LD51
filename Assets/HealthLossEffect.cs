using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthLossEffect : MonoBehaviour
{
    public float moveDistance;
    private float progress;
    public TMP_Text label;

    public AnimationCurve alphaCurve;

    private void Start()
    {
        progress = 0;
    }

    private void Update()
    {
        Color color;
        progress += Time.deltaTime;
        if (progress < 1)
        {
            Vector3 position = transform.position;
            position.y = progress * moveDistance;
            transform.position = position;

            color = label.color;
            color.a = alphaCurve.Evaluate(progress);
            label.color = color;
        }
        else
        {
            color = label.color;
            color.a = alphaCurve.Evaluate(1);
            label.color = color;
            transform.position = new Vector3(transform.position.x, moveDistance, transform.position.z);
            Destroy(gameObject);
        }
    }

    public void SetValue(int value)
    {
        label.text = value.ToString();
    }

}
