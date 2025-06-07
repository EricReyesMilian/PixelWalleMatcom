using System;
using System.Collections.Generic;

public class Parser
{
    private readonly List<Token> tokens;
    private int _currentLine;
    private int _currentPosition;
    List<ASTNode> block = new List<ASTNode>();
    List<List<Token>> tokensByLine = new List<List<Token>>();
    private SemanticException semanticException = new SemanticException();
    bool errorInLine = false;
    // en caso de error saltar la linea q se esta procesando
    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;


    }

    public List<ASTNode> Parse()
    {

        tokensByLine = ProccesTokensByLine(tokens);//explorar linea a linea
        for (_currentLine = 0; _currentLine < tokensByLine.Count; _currentLine++)
        {
            for (_currentPosition = 0; _currentPosition < tokensByLine[_currentLine].Count; _currentPosition++)
            {
                if (tokensByLine[_currentLine][_currentPosition].Value != " " && tokensByLine[_currentLine][_currentPosition].Type != TokenType.Null && tokensByLine[_currentLine][_currentPosition].Type != TokenType.END)
                {
                    errorInLine = false;
                    ASTNode proccesNode = ParseKeyW();
                    if (!errorInLine)
                    {
                        block.Add(proccesNode);

                    }
                }
            }

        }
        // for (int i = 0; i < tokensByLine[_currentLine].Count; i++)
        // {
        //     if (tokensByLine[_currentLine][_currentPosition].Value != " " && tokensByLine[_currentLine][_currentPosition].Type != TokenType.Null)
        //     {
        //         errorInLine = false;
        //         ASTNode proccesNode = ParseKeyW();
        //         if (!errorInLine)
        //         {
        //             block.Add(proccesNode);

        //         }
        //     }
        //     i = ++_currentPosition;
        // }
        return block;
    }
    private List<List<Token>> ProccesTokensByLine(List<Token> tokens)
    {
        List<List<Token>> tokensByLine = new List<List<Token>>();
        for (int i = 0; i < tokens.Count; i++)
        {
            while (tokens[i].Line > tokensByLine.Count - 1)
            {
                tokensByLine.Add(new List<Token>());
            }
            tokensByLine[tokens[i].Line].Add(tokens[i]);
        }
        return tokensByLine;

    }
    private LabelNode FindLabel(string labelName)
    {
        Token labelObj;
        for (int i = _currentLine + 1; i < tokensByLine.Count; i++)
        {

            if (tokensByLine[i][0].Type == TokenType.Identifier)
            {
                if (tokensByLine[i][0].Value == labelName)
                {
                    labelObj = tokensByLine[i][0];
                    return new LabelNode(labelObj, i);
                }
            }
        }
        throw new Exception($"{labelName} no es un label valido {Peek()}");
    }
    private ASTNode ParseKeyW()
    {
        if (Match(TokenType.Keyword) || Match(TokenType.Function))
        {
            Token kw = tokensByLine[_currentLine][_currentPosition];
            if (Match(TokenType.Function))
            {
                if (_currentPosition - 1 > 0)
                {
                    if (tokensByLine[_currentLine][_currentPosition - 1].Value == " ")
                    {
                        throw new Exception($"Debes declarar una instruccion {Peek()}");
                    }
                }
            }
            Consume(); // Consume la funcion

            ASTNode parametro = new FunctionNode(kw, GetParams());

            return parametro;
        }
        else
        {
            return ParseLoop();
        }


    }
    private ASTNode ParseLoop()
    {
        if (Match(TokenType.Goto))
        {
            Token tokengoto = tokensByLine[_currentLine][_currentPosition];
            LabelNode label;
            ASTNode expresion;

            if (++_currentPosition < tokensByLine[_currentLine].Count && Match(TokenType.Punctuation, "["))
            {
                if (++_currentPosition < tokensByLine[_currentLine].Count && Match(TokenType.Identifier))
                {
                    //buscar el label
                    if (FunctionManager.labels.ContainsKey(tokensByLine[_currentLine][_currentPosition].Value))
                    {
                        label = new LabelNode(tokensByLine[_currentLine][_currentPosition], FunctionManager.labels[tokensByLine[_currentLine][_currentPosition].Value]);

                    }
                    else
                    {
                        label = FindLabel(tokensByLine[_currentLine][_currentPosition].Value);
                    }
                    //
                    if (++_currentPosition < tokensByLine[_currentLine].Count && Match(TokenType.Punctuation, "]"))
                    {
                        if (++_currentPosition < tokensByLine[_currentLine].Count && Match(TokenType.Punctuation, "("))
                        {
                            expresion = ParseLogic();
                            --_currentPosition;
                            if (Match(TokenType.Punctuation, ")"))
                            {
                                Consume();
                                return new LoopNode(tokengoto, label, expresion);
                            }
                            else
                            {
                                throw new Exception($"Se esperaba ) {Peek()}");

                            }
                        }
                        else
                        {
                            throw new Exception($"Se esperaba ( {Peek()}");

                        }

                    }
                    else
                    {
                        throw new Exception($"Se esperaba ] {Peek()}");

                    }
                }
                else
                {
                    throw new Exception($"Se esperaba un Label {Peek()}");
                }
            }
            else
            {

                throw new Exception($"Se esperaba [ {Peek()}");
            }
        }
        else
        {
            return ParseVar();

        }

    }
    private ASTNode ParseVar()
    {
        if (Match(TokenType.Identifier))
        {
            var variable = tokensByLine[_currentLine][_currentPosition];

            Consume();
            if (Match(TokenType.Operator, "<-"))
            {
                Token op = tokensByLine[_currentLine][_currentPosition++];
                //controlar out of index
                ASTNode right;

                right = ParseSum();

                return new AssingNode(op, new VariableNode(variable), right);
            }
            else
            {
                _currentPosition--;
                return ParseLabel();
            }
        }
        else
        {

            throw new Exception($"Se Esperaba una instruccion {Peek()}");
        }
    }
    private ASTNode ParseLabel()
    {
        if (tokensByLine[_currentLine][_currentPosition + 1].Type == TokenType.Jump || tokensByLine[_currentLine][_currentPosition + 1].Type == TokenType.END)
        {
            if (FunctionManager.labels.ContainsKey(tokensByLine[_currentLine][_currentPosition].Value))
            {
                throw new Exception($"Ese label ya estaba declarado {Peek()}");
            }
            FunctionManager.labels.Add(tokensByLine[_currentLine][_currentPosition].Value, block.Count);
            return new LabelNode(tokensByLine[_currentLine][_currentPosition], block.Count);
        }
        else
            throw new Exception($"Se esperaba una instruccion {Peek()}");
    }
    private List<ASTNode> GetParams()
    {
        var param = new List<ASTNode>();
        Consume();
        while (true)
        {
            if (Match(TokenType.Color))
            {
                param.Add(ParseColor());
            }
            else
            if (!Match(TokenType.Punctuation, ")"))
                param.Add(ParseSum());

            if (Match(TokenType.Punctuation, ","))
            {
                Consume();
            }
            else
            {
                if (tokensByLine[_currentLine][_currentPosition].Value != ")" || tokensByLine[_currentLine][_currentPosition].Value != "\n" || tokensByLine[_currentLine][_currentPosition].Value != "\r")
                {
                    break;
                }
                else
                {
                    throw new Exception($"Se esperaba ',' se encontro {Peek()}");

                }
            }


        }

        if (!Match(TokenType.Punctuation, ")"))
            throw new Exception($"Se esperaba ')'{Peek()}");
        Consume(); // Consume ')'

        return param;
    }
    private ASTNode ParseSum()
    {
        var left = ParseMul();

        while (Match(TokenType.Operator, "+") || Match(TokenType.Operator, "-"))
        {
            var opToken = Consume();
            var right = ParseMul();
            left = new BinaryOperatorNode(opToken, left, right);
        }

        return left;
    }
    private ASTNode ParseMul()
    {
        var left = ParsePow();

        while (Match(TokenType.Operator, "*") || Match(TokenType.Operator, "%") || Match(TokenType.Operator, "/"))
        {
            var opToken = Consume();
            var right = ParsePow();
            left = new BinaryOperatorNode(opToken, left, right);
        }

        return left;
    }
    private ASTNode ParsePow()
    {
        var left = ParseLogic();

        while (Match(TokenType.Operator, "**"))
        {
            var opToken = Consume();
            var right = ParseComparer();
            left = new BinaryOperatorNode(opToken, left, right);
        }

        return left;
    }
    private ASTNode ParseComparer()
    {
        var left = ParseFactor();

        while (Match(TokenType.Operator, "==") ||
        Match(TokenType.Operator, "<") || Match(TokenType.Operator, ">") || Match(TokenType.Operator, ">=")
        || Match(TokenType.Operator, "<="))
        {
            var opToken = Consume();
            var right = ParseFactor();
            left = new BinaryOperatorNode(opToken, left, right);
        }
        return left;
    }
    private ASTNode ParseLogic()
    {
        var left = ParseComparer();

        while (Match(TokenType.Operator, "&&") || Match(TokenType.Operator, "||"))
        {
            var opToken = Consume();
            var right = ParseComparer();
            left = new BinaryOperatorNode(opToken, left, right);
        }
        return left;
    }
    private ASTNode ParseFactor()
    {
        if (Match(TokenType.Number) || Match(TokenType.Function) || Match(TokenType.Identifier))
        {
            if (Match(TokenType.Number))
            {
                return new ConstantNode(Consume());

            }
            else if (Match(TokenType.Function))
            {
                return new FunctionNode(Consume(), GetParams());
            }
            else
            if (Match(TokenType.Identifier))
            {
                return new VariableNode(Consume());

            }
            else
            {
                return ParseKeyW();
            }
        }

        if (Match(TokenType.Punctuation, "("))
        {
            Consume(); // Consume '('
            var expr = ParseSum();
            if (!Match(TokenType.Punctuation, ")"))
                throw new Exception($"Se esperaba ')' no: {Peek()}");
            Consume(); // Consume ')'
            return expr;
        }

        throw new Exception($"Token inesperado: {Peek()}");
    }
    private ASTNode ParseColor()
    {
        if (Match(TokenType.Color))
        {
            return new ColorNode(Consume());
        }
        else
        {
            throw new Exception($"Color inesperado: {Peek()}");

        }
    }
    private bool Match(TokenType type, string value = null, Token tokenaux = null)
    {
        if (_currentPosition >= tokensByLine[_currentLine].Count) return false;
        Token token;
        if (tokenaux == null)
            token = tokensByLine[_currentLine][_currentPosition];
        else
            token = tokenaux;

        if (token.Type != type) return false;

        if (value != null && token.Value != value)
            return false;

        return true;
    }


    private Token Consume()
    {
        return tokensByLine[_currentLine][_currentPosition++];
    }

    private Token Peek()
    {

        return _currentPosition < tokensByLine[_currentLine].Count ? tokensByLine[_currentLine][_currentPosition] : throw new Exception("Fuera de los limites");
    }
}