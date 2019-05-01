using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText;
    
    [SerializeField]
    private TextMeshProUGUI rangeText;

    [SerializeField]
    private TextMeshProUGUI dmgPerSecText;

    public void Setup(TowerInfo info)
    {
        nameText.text = info.GetName();

        rangeText.text = info.GetRange().ToString();

        dmgPerSecText.text = info.GetDamagePerSecond().ToString();
    }

}
