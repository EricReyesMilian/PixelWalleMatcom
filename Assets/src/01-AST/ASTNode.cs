using System;
using UnityEngine;
public abstract class ASTNode //: MonoBehaviour
{
    public Token token { get; }

    public ASTNode(Token token)
    {
        this.token = token;
    }
    public abstract object Execute();

    public virtual void PrintTree(string indent = "", bool last = true)
    {
        // print(indent);
        // print(last ? "└─ " : "├─ ");
        // print($"{GetType().Name} > {token}");
    }

}
