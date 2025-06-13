using System;
public class ColorNode : ASTNode
{
    public ColorNode(Token token) : base(token) { }
    public override object Execute()
    {

        return token.Value switch
        {
            "\"Transparent\"" => 0,
            "\"Blue\"" => 1,
            "\"Red\"" => 2,
            "\"Green\"" => 3,
            "\"Yellow\"" => 4,
            "\"Orange\"" => 5,
            "\"Purple\"" => 6,
            "\"Black\"" => 7,
            "\"White\"" => 8,
            _ => throw new RunTimeException($"Color no soportado: {token.Value}")

        };
    }
}