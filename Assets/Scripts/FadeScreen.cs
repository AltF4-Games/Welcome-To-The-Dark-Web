using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup img;

    private void Start()
    {
        img.alpha = 1f;
        FadeBlack(0f, 1f);
    }

    public void FadeBlack(float val, float time)
    {
        LeanTween.alphaCanvas(img, val, time);
    }
}

