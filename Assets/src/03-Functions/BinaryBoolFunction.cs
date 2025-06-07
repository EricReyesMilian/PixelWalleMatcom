public abstract class BinaryBoolFunction : BaseFunction
{
    public BinaryBoolFunction(string name) : base(name, 2)
    {
    }
    public abstract bool Execute(object left, object right);
}