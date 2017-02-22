public class SizedResource : Resource
{
    public ResourceSize Size { get; set; }

    public SizedResource(string name) : base(name)
    {
    }

    public override string GetDisplayText()
    {        
        return base.GetDisplayText() + " - " + Size.ToString();
    }
}
