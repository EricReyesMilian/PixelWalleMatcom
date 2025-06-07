using System.Collections.Generic;
using System;
public class FunctionNode : ASTNode
{
    List<ASTNode> param = new List<ASTNode>();
    public FunctionNode(Token token, List<ASTNode> param) : base(token)
    {
        this.param = param;

    }
    public override object Execute()
    {
        if (token.Type == TokenType.Function)
        {
            int[] par = new int[param.Count];
            for (int i = 0; i < param.Count; i++)
            {
                par[i] = (int)param[i].Execute();
            }
            return FunctionManager.GetIntFunction(token.Value, par);
        }
        else
        {
            int[] par = new int[param.Count];
            for (int i = 0; i < param.Count; i++)
            {
                try
                {
                    par[i] = (int)param[i].Execute();

                }
                catch
                {
                    throw new Exception("Se esta tomando un valor booleano en vez de uno entero");
                }
            }
            FunctionManager.GetVoidFunction(token.Value, par);
            return null;
        }

    }
    public override void PrintTree(string indent = "", bool last = true)
    {
        base.PrintTree(indent, last);
        foreach (var par in param)
        {
            par.PrintTree(indent + (last ? "   " : "│  "), false);
        }
    }

}





