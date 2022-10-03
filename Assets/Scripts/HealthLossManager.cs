using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLossManager : MonoBehaviour
{
    public HealthLossEffect healthLossPrefab;
    public Camera viewCamera;
    public RectTransform container;

    void Start()
    {
        Battler.OnBattlerDamaged += ShowDamageEffects;
    }

    private void ShowDamageEffects(Battler battler, int value)
    {
        var healthLossEffect = Instantiate(healthLossPrefab, transform);
        var screenPosition = viewCamera.WorldToScreenPoint(battler.transform.position);
        float x = screenPosition.x / Screen.width * container.rect.x;
        float y = screenPosition.y / Screen.height * container.rect.y;
        Vector3 position = new Vector3(x, y, 0);
        healthLossEffect.transform.position = position;
        healthLossEffect.SetValue(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
