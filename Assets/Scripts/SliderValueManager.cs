using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueManager : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.transform.parent.GetComponent<Slider>();  
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = this.slider.value.ToString();
    }
}
