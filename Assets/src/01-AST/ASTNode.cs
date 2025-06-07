using System;
public abstract class ASTNode
{
    public Token token { get; }

    public ASTNode(Token token)
    {
        this.token = token;
    }
    public abstract object Execute();

    public virtual void PrintTree(string indent = "", bool last = true)
    {
        Console.Write(indent);
        Console.Write(last ? "└─ " : "├─ ");
        Console.WriteLine($"{GetType().Name} > {token}");
    }

}
