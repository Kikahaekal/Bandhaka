using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider volumeSlider;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        volume = volumeSlider.value;
    }

    public void SetVolume()
    {
        volume = volumeSlider.value;
    }
}
