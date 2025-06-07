using System.Collections.Generic;
public abstract class IntFunction : BaseFunction
{

    public IntFunction(string name, int paramCount) : base(name, paramCount) { }


    public abstract int Execute(int[] arr);

}
