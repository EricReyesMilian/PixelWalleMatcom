public class AssingNode : ASTNode
{
    public VariableNode variable { get; }
    public ASTNode right { get; }
    public AssingNode(Token token, VariableNode variable, ASTNode right) : base(token)
    {
        this.variable = variable;
        this.right = right;
    }
    public override void PrintTree(string indent = "", bool last = true)
    {
        base.PrintTree(indent, last);
        variable.PrintTree(indent + (last ? "   " : "│  "), false);
        right.PrintTree(indent + (last ? "   " : "│  "), true);
    }
    public override object Execute()
    {
        object value = right.Execute();
        FunctionManager.variables[variable.token.Value] = FunctionManager.CheckInt(value);
        return (int)value;
    }

}