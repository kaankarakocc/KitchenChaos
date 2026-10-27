using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizzlingParticles;


    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveOnVisual.SetActive(false);
        sizzlingParticles.SetActive(false);
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            stoveOnVisual.SetActive(true);
            sizzlingParticles.SetActive(true);
        }
        else
        {
            stoveOnVisual.SetActive(false);
            sizzlingParticles.SetActive(false);
        }
    }
}