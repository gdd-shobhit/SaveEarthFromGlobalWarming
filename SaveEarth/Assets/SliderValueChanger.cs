using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SliderValueChanger : MonoBehaviour
{
    public void ChangeValue (TextMeshProUGUI text)
    {
        text.text = this.GetComponent<Slider>().value.ToString();
    }
}
