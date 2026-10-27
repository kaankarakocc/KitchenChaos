using UnityEngine;

public class ContainerCounterAnimator : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    private const string TRIGGER_OPENCLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TRIGGER_OPENCLOSE);
    }
}
