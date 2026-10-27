
public class DeliveryOrder
{
    private int identity;
    private RecipeSO recipeSO;

    public DeliveryOrder(int orderId, RecipeSO recipe)
    {
        identity = orderId;
        recipeSO = recipe;
    }

    public int GetIdentity()
    {
        return identity;
    }

    public RecipeSO GetRecipeSO()
    {
        return recipeSO;
    }

}
