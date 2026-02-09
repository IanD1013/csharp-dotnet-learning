using Builder.Classic.Builders;

namespace Builder.Classic;

public class ProductDirector(IBuilder builder)
{
    public void ConstructProduct()
    {
        builder.BuildName();
        builder.BuildDescription(); 
    }
}