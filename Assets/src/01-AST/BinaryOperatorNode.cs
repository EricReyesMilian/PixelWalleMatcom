using System;
public class BinaryOperatorNode : ASTNode
{
    public ASTNode Left { get; }
    public ASTNode Right { get; }

    public BinaryOperatorNode(Token opToken, ASTNode left, ASTNode right) : base(opToken)
    {
        Left = left;
        Right = right;
    }

    public override void PrintTree(string indent = "", bool last = true)
    {
        base.PrintTree(indent, last);
        Left.PrintTree(indent + (last ? "   " : "│  "), false);
        Right.PrintTree(indent + (last ? "   " : "│  "), true);
    }
    public override object Execute()
    {
        object leftVal = Left.Execute();
        object rightVal = Right.Execute();

        switch (token.Value)
        {
            //chequear que no sean colores
            case "+": return FunctionManager.CheckInt(leftVal) + FunctionManager.CheckInt(rightVal);
            case "-": return FunctionManager.CheckInt(leftVal) - FunctionManager.CheckInt(rightVal);
            case "*": return FunctionManager.CheckInt(leftVal) * FunctionManager.CheckInt(rightVal);
            case "/":
                if (FunctionManager.CheckInt(rightVal) == 0)
                {
                    throw new RunTimeException($"Intentaste dividir por cero: {token.Value}");
                }
                return FunctionManager.CheckInt(leftVal) / FunctionManager.CheckInt(rightVal);
            case "**": return FunctionManager.CheckInt((int)Math.Round(Math.Pow(FunctionManager.CheckInt(leftVal), FunctionManager.CheckInt(rightVal))));
            case "%": return FunctionManager.CheckInt(leftVal) % FunctionManager.CheckInt(rightVal);

            case "==": return FunctionManager.CheckInt(leftVal) == FunctionManager.CheckInt(rightVal);
            case ">": return FunctionManager.CheckInt(leftVal) > FunctionManager.CheckInt(rightVal);
            case "<": return FunctionManager.CheckInt(leftVal) < FunctionManager.CheckInt(rightVal);
            case ">=": return FunctionManager.CheckInt(leftVal) >= FunctionManager.CheckInt(rightVal);
            case "<=": return FunctionManager.CheckInt(leftVal) <= FunctionManager.CheckInt(rightVal);
            case "&&": return FunctionManager.CheckBool(leftVal) && FunctionManager.CheckBool(rightVal);
            case "||": return FunctionManager.CheckBool(leftVal) || FunctionManager.CheckBool(rightVal);

            default: throw new RunTimeException($"Operador no soportado: {token.Value}");
        }
    }


}