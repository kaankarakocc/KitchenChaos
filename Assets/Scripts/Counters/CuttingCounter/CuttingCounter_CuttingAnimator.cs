using UnityEngine;

public class CuttingCounter_CuttingAnimator : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private const string TRIGGER_CUT = "Cut";

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
        animator = GetComponent<Animator>();
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TRIGGER_CUT);
    }
}
