using UnityEngine;
using UnityEngine.Events;

public class CompanionButton : MonoBehaviour
{
    public Transform standPoint;      // companion'ın duracağı nokta (boşsa butonun kendisi)
    public UnityEvent onPressed;      // Inspector'dan kapıyı bağlarsın
    [HideInInspector] public bool pressed;

    public void Press()
    {
        if (pressed) return;
        pressed = true;
        onPressed.Invoke();
    }
}