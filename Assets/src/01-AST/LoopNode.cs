using System;
public class LoopNode : ASTNode
{
    public LabelNode label;
    public ASTNode condition;
    public LoopNode(Token token, LabelNode label, ASTNode condition) : base(token)
    {

        this.label = label;
        this.condition = condition;
    }
    public override object Execute()
    {
        if (FunctionManager.CheckBool(condition.Execute()))
            Interpreter.rama = label.line;
        Console.WriteLine($"La rama destino es {label.line}");
        return 0;
    }
    public override void PrintTree(string indent = "", bool last = true)
    {
        base.PrintTree(indent, last);
        label.PrintTree(indent + (last ? "   " : "│  "), false);
        condition.PrintTree(indent + (last ? "   " : "│  "), true);
    }
}