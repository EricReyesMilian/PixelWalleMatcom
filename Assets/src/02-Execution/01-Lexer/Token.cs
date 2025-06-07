public enum TokenType
{
    Number,
    Goto,
    Keyword,//Metodos sin retorno
    Operator,
    Punctuation,//[](),
    Identifier,//variables
    Jump,
    Function,//funciones con retorno
    Color,
    END,
    Null

}
public class Token
{
    public TokenType Type { get; private set; }
    public string Value { get; }
    public int Line { get; }
    public int Column { get; }
    public int? paramCount;
    public Token(TokenType type, string value, int line, int column, int? paramCount = null)
    {
        Type = type;
        Value = value;
        Line = line;
        Column = column;
        this.paramCount = paramCount;
    }
    public void SetNewType(TokenType newType)
    {
        Type = newType;
    }
    public override string ToString() => $"{Type}: '{Value}' (L:{Line}, C:{Column})";

}