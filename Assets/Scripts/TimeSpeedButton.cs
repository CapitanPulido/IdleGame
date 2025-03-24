using UnityEngine;
using UnityEngine.UI;

public class TimeSpeedButton : MonoBehaviour
{
    public Sprite spriteX2;  // Sprite para velocidad x2
    public Sprite spriteX3;  // Sprite para velocidad x3
    public Sprite spriteX1;  // Sprite para velocidad normal

    private int clickCount = 0;
    private Image buttonImage;
    public float TimeActual;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = spriteX1;  // Iniciar con sprite normal
    }

    public void ChangeTimeScale()
    {
        clickCount++;

        switch (clickCount % 3)
        {
            case 1:
                Time.timeScale = 2f;
                buttonImage.sprite = spriteX2;
                TimeActual = 2;
                Debug.Log("X2");
                break;
            case 2:
                Time.timeScale = 3f;
                buttonImage.sprite = spriteX3;
                TimeActual = 3;
                Debug.Log("X3");
                break;
            case 0:
                Time.timeScale = 1f;
                buttonImage.sprite = spriteX1;
                TimeActual = 1;
                break;
        }
    }
}
