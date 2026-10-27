using UnityEngine;

[CreateAssetMenu()]
public class CuttingObjectSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public int cuttingProgressMax;
}
