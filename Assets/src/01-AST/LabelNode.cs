public class LabelNode : ASTNode
{
    public int line;
    public LabelNode(Token token, int line) : base(token)
    {
        this.line = line;
    }
    public override object Execute()
    {
        return token.Value;
    }
}
