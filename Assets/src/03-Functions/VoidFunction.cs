public abstract class VoidFunction : BaseFunction
{
    public VoidFunction(string name, int paramCount, int[] parCod) : base(name, paramCount, parCod) { }
    public abstract void Execute(int[] arr);
}