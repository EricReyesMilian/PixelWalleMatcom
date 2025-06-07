public abstract class VoidFunction : BaseFunction
{
    public VoidFunction(string name, int paramCount) : base(name, paramCount) { }
    public abstract void Execute(int[] arr);
}