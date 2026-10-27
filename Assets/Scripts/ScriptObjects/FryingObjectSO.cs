using UnityEngine;

[CreateAssetMenu()]
public class FryingObjectSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float fryingTime;
}
