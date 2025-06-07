using System;
using System.Collections.Generic;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _currentPosition;
    List<ASTNode> block = new List<ASTNode>();

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _currentPosition = 0;
    }

    public List<ASTNode> Parse()
    {


        for (int i = 0; i < _tokens.Count; i++)
        {
            if (_tokens[_currentPosition].Value != " " && _tokens[_currentPosition].Type != TokenType.Null)
                block.Add(ParseKeyW());
            i = ++_currentPosition;
        }
        return block;
    }
    private LabelNode FindLabel(string labelName)
    {
        int lines = 0;
        Token labelObj;
        for (int i = _currentPosition + 1; i < _tokens.Count; i++)
        {
            if (_tokens[i].Value == " ")
            {
                lines++;
            }
            if (_tokens[i].Type == TokenType.Identifier)
            {
                if (_tokens[i].Value == labelName)
                {
                    labelObj = _tokens[i];
                    return new LabelNode(labelObj, lines + block.Count);
                }
            }
        }
        throw new Exception($"{labelName} no es un label valido {Peek()}");
    }
    private ASTNode ParseKeyW()
    {
        if (Match(TokenType.Keyword) || Match(TokenType.Function))
        {
            Token kw = _tokens[_currentPosition];
            if (Match(TokenType.Function))
            {
                if (_currentPosition - 1 > 0)
                {
                    if (_tokens[_currentPosition - 1].Value == " ")
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
            Token tokengoto = _tokens[_currentPosition];
            LabelNode label;
            ASTNode expresion;

            if (++_currentPosition < _tokens.Count && Match(TokenType.Punctuation, "["))
            {
                if (++_currentPosition < _tokens.Count && Match(TokenType.Identifier))
                {
                    //buscar el label
                    if (FunctionManager.labels.ContainsKey(_tokens[_currentPosition].Value))
                    {
                        label = new LabelNode(_tokens[_currentPosition], FunctionManager.labels[_tokens[_currentPosition].Value]);

                    }
                    else
                    {
                        label = FindLabel(_tokens[_currentPosition].Value);
                    }
                    //
                    if (++_currentPosition < _tokens.Count && Match(TokenType.Punctuation, "]"))
                    {
                        if (++_currentPosition < _tokens.Count && Match(TokenType.Punctuation, "("))
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
            var variable = _tokens[_currentPosition];

            Consume();
            if (Match(TokenType.Operator, "<-"))
            {
                Token op = _tokens[_currentPosition++];
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
        if (_tokens[_currentPosition + 1].Type == TokenType.Jump || _tokens[_currentPosition + 1].Type == TokenType.END)
        {
            if (FunctionManager.labels.ContainsKey(_tokens[_currentPosition].Value))
            {
                throw new Exception($"Ese label ya estaba declarado {Peek()}");
            }
            FunctionManager.labels.Add(_tokens[_currentPosition].Value, block.Count);
            return new LabelNode(_tokens[_currentPosition], block.Count);
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
                if (_tokens[_currentPosition].Value != ")" || _tokens[_currentPosition].Value != "\n" || _tokens[_currentPosition].Value != "\r")
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
    private bool Match(TokenType type, string? value = null, Token? tokenaux = null)
    {
        if (_currentPosition >= _tokens.Count) return false;
        Token token;
        if (tokenaux == null)
            token = _tokens[_currentPosition];
        else
            token = tokenaux;

        if (token.Type != type) return false;

        if (value != null && token.Value != value)
            return false;

        return true;
    }


    private Token Consume()
    {
        return _tokens[_currentPosition++];
    }

    private Token Peek()
    {

        return _currentPosition < _tokens.Count ? _tokens[_currentPosition] : throw new Exception("Fuera de los limites");
    }
}