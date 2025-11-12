using UnityEngine;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private GameObject _loseWindow;
    
    public void ShowWindow()
    {
        _loseWindow.SetActive(true);
    }
}