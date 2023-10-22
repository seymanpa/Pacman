using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public TMP_Text volumeAmount;

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        volumeAmount.text = ((int)(value * 100)).ToString();
    }  
}
