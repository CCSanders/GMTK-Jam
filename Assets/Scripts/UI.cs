using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Text ammoText;
    [SerializeField] Slider cloneSlider;
    [SerializeField] Color activeColor;
    [SerializeField] Color cloneUnavilableColor;

    Player player;
    ShadowClone clone;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        clone = FindObjectOfType<ShadowClone>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "" + player.ammoCount;
        float percent = clone.GetCloneTimePercent();
        cloneSlider.value = percent;

        if(!clone.GetIsActive() && percent < 1)
        {
            cloneSlider.fillRect.GetComponent<Image>().color = cloneUnavilableColor;
        }
        else
        {
            cloneSlider.fillRect.GetComponent<Image>().color = activeColor;
        }
    }
}
