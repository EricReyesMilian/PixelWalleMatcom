using System.Collections.Generic;
public abstract class IntFunction : BaseFunction
{

    public IntFunction(string name, int paramCount, int[] parCod) : base(name, paramCount, parCod) { }


    public abstract int Execute(int[] arr);

}
