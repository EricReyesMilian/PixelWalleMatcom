public class ConstantNode : ASTNode
{
    public ConstantNode(Token token) : base(token) { }
    public override object Execute()
    {
        return int.Parse(token.Value); // Convierte el token a número
    }

}