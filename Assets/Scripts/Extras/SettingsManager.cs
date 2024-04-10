using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void ToggleSettings()
    {
        this.gameObject.SetActive(!this.isActiveAndEnabled);
    }
}
