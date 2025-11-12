using TMPro;
using UnityEngine;

public class UITextChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void ChangeInt(int value)
    {
        if(value == 1)
            _text.gameObject.SetActive(false);
        else
            _text.gameObject.SetActive(true);
        
        _text.text = value.ToString();
    }
}