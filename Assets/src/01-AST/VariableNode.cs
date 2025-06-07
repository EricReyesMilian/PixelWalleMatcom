using System;
public class VariableNode : ASTNode
{
    public VariableNode(Token token) : base(token) { }
    public override object Execute()
    {
        // Buscar el valor en el diccionario de variables
        if (FunctionManager.variables.TryGetValue(token.Value, out int value))
        {
            return value;
        }
        throw new Exception($"Variable no definida: {token.Value}");
    }

}