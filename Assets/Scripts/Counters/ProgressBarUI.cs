using UnityEngine;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressSourceGameObject;
    [SerializeField] private UnityEngine.UI.Image barImage;
    private IHasProgress progressSource;

    void Start()
    {
        progressSource = progressSourceGameObject.GetComponent<IHasProgress>();
        if (progressSource != null)
        {
            progressSource.OnProgressChanged += CuttingCounter_OnProgressChanged;
        }
        barImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progress;
        if (barImage.fillAmount == 0 || barImage.fillAmount == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
