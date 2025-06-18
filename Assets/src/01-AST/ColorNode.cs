using System;
using System.Drawing;
public class ColorNode : ASTNode
{
    public ColorNode(Token token) : base(token) { }
    public override object Execute()
    {

        return ColorHandler.GetColorNumber(token);

    }
}