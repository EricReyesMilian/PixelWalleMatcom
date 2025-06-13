using System.Collections.Generic;
using System;

public class Lexer
{
    private string _input;
    private int _position;
    private int _line = 1;
    private int _column = 1;
    public LexicalExceptionHandler Exception = new LexicalExceptionHandler();
    List<Token> tokens = new();
    public delegate bool EvaluateToken(string value);
    //Palabras clave, debe contener a todas las funciones con retorno de valor
    private readonly HashSet<string> _keywords = new()
    { "GoTo", "Spawn", "Color", "Size", "DrawLine", "DrawCircle", "DrawRectangle", "Fill", };
    //funciones con retorno de valor
    private readonly HashSet<string> _NoVoidFunction = new()
    {"GetActualX", "GetActualY", "GetCanvasSize", "GetColorCount", "IsBrushColor", "IsBrushSize", "IsCanvasColor" };
    //Colores
    private readonly HashSet<string> _colors = new()
    { "\"Transparent\"", "\"Blue\"", "\"Red\"", "\"Green\"", "\"Yellow\"", "\"Orange\"", "\"Purple\"", "\"Black\"", "\"White\"" };
    // Operadores
    private readonly HashSet<string> _operators = new()
    { "+", "-", "*", "**", "%", "/", "<", ">", "<=", ">=", "==","=", "<-","&","|", "&&", "||" };
    // Delimitadores
    private readonly HashSet<char> _punctuation = new()
    { '(', ')', ',', '[', ']' };

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
    }
    public List<Token> Tokenize()
    {
        Token token;

        do
        {

            token = GetNextToken();
            if (tokens != null)
                tokens.Add(token);

        } while (token.Type != TokenType.END);

        return tokens;
    }
    private Token GetNextToken()
    {
        SkipWhitespace();
        if (_position >= _input.Length)
            return new Token(TokenType.END, "", _line, _column);

        char current = _input[_position];

        // Máquina de Estados Finitos
        if (current == '\n')
            return ReadNewline();
        if (char.IsDigit(current))
            return ReadNumber();
        if (current == '"')
            return ReadColor();
        if (char.IsLetter(current) || current == '_')
            return ReadIdentifier();
        if (_operators.Contains(current.ToString()))
            return ReadOperator();
        if (_punctuation.Contains(current))
            return ReadPunctuation();

        // Carácter no reconocido
        Exception.Report($"Carácter inesperado: '{current}'", _line, _column);
        _position++;
        return new Token(TokenType.Null, " ", _line, _column++);

    }

    #region Estados
    private Token ReadNewline()
    {
        int startLine = _line;
        int startCol = _column;

        // Manejar \r\n (Windows) y \n (Unix)
        if (_position + 1 < _input.Length && _input[_position] == '\r' && _input[_position + 1] == '\n')
        {
            _position += 2;
        }
        else
        {
            _position++;
        }

        _line++;
        _column = 1;

        return ScanToken(TokenType.Jump, " ", startLine, startCol, IsValid);
    }


    private Token ReadColor()
    {
        int start = _position;
        int startLine = _line;
        int startCol = _column;

        Advance();

        while (_position < _input.Length && _input[_position] != '\n')
        {
            Advance();
            if (_input[_position] == '"')
            {
                Advance();
                break;
            }
        }
        string value = _input.Substring(start, _position - start);

        //agregar esto a todos los metodos
        return ScanToken(TokenType.Color, value, startLine, startCol, IsValidColor);


    }
    private Token ReadOperator()
    {
        char op = _input[_position];
        int currentLine = _line;
        int currentCol = _column;
        Advance();

        // Operadores de dos caracteres (==, <=, >=, <-)
        if (_position < _input.Length && _operators.Contains(_input[_position].ToString()))
        {
            string doubleOp = op.ToString() + _input[_position];
            if (_operators.Contains(doubleOp))
            {
                Advance();
                return ScanToken(TokenType.Operator, doubleOp, currentLine, currentCol, IsValidOperator);
            }
        }
        else if (op == '-' && char.IsDigit(_input[_position]))
        {
            _position--;
            return ReadNumber();
        }
        return ScanToken(TokenType.Operator, op.ToString(), currentLine, currentCol, IsValidOperator);
    }
    private Token ReadNumber()
    {
        int start = _position;
        int startLine = _line;
        int startCol = _column;
        if (_input[start] == '-')
        {
            Advance();
        }
        while (_position < _input.Length && _input[_position] != ' ' && !_operators.Contains(_input[_position].ToString()) && _input[_position] != '\n' && !_punctuation.Contains(_input[_position]))
        {
            Advance();

        }

        string value = _input.Substring(start, _position - start);
        if (_input[_position] == '\n')
        {
            value = _input.Substring(start, (_position - 1) - start);
            _position -= 1;

        }

        if (!_NoVoidFunction.Contains(value))
        {
            return ScanToken(TokenType.Number, value, startLine, startCol, IsValidNumber);
        }
        else
        {
            return ScanToken(TokenType.Function, value, startLine, startCol, IsValidFunction);

        }
    }
    private Token ReadIdentifier()
    {
        int start = _position;
        int startLine = _line;
        int startCol = _column;

        while (_position < _input.Length && (char.IsLetterOrDigit(_input[_position]) || _input[_position] == '_' || _input[_position] == '-'))
            Advance();

        string value = _input.Substring(start, _position - start);
        TokenType type;
        if (_keywords.Contains(value) || _NoVoidFunction.Contains(value))
        {

            if (_NoVoidFunction.Contains(value))
            {
                type = TokenType.Function;
                return ScanToken(type, value, startLine, startCol, IsValidFunction);

            }
            else
            {
                type = TokenType.Keyword;
                if (value == "GoTo")
                {
                    type = TokenType.Goto;
                }
                return ScanToken(type, value, startLine, startCol, IsValidKeyWord);

            }

        }
        else
        {

            type = TokenType.Identifier;
            return ScanToken(type, value, startLine, startCol, IsValidIdenti);

        }

    }
    private Token ReadPunctuation()
    {
        char punct = _input[_position];
        int currentLine = _line;
        int currentCol = _column;
        Advance();
        return ScanToken(TokenType.Punctuation, punct.ToString(), currentLine, currentCol, IsValidPunctuation);
    }

    #endregion
    #region Mousequerramientas
    private void Advance()
    {
        if (_input[_position] == '\n')
        {
            _line++;
            _column = 1;
        }
        else
        {
            _column++;
        }
        _position++;
    }
    private void SkipWhitespace()
    {
        while (_position < _input.Length && (char.IsWhiteSpace(_input[_position]) && _input[_position] != '\n'))
        {
            _column++;
            _position++;
        }
    }
    bool IsValid(string input)
    {
        // Caracteres considerados inválidos
        string invalidChars = "#@!$~;:'^";
        // Verificar si la cadena contiene alguno de los caracteres inválidos
        foreach (char c in input)
        {
            if (invalidChars.Contains(c))
            {
                return false; // Se encontró un carácter inválido
            }
        }

        return true; // Todos los caracteres son válidos
    }
    bool IsValidNumber(string input)
    {
        // Caracteres considerados inválidos
        string validChars = "1234567890";

        for (int i = 0; i < input.Length; i++)
        {
            if (i == 0 && input[0] == '-')
            {
                continue;
            }
            if (!validChars.Contains(input[i]))
            {
                return false;
            }
        }

        return true; // Todos los caracteres son válidos
    }
    bool IsValidIdenti(string input)
    {
        // Caracteres considerados inválidos
        string frist = "1234567890-";
        if (frist.Contains(input[0]))
        {
            return false;
        }

        return true; // Todos los caracteres son válidos
    }
    bool IsValidPunctuation(string input)
    {
        // Caracteres considerados inválidos
        if (_punctuation.Contains(input[0]))
        {
            return true;
        }

        return false; // Todos los caracteres son válidos
    }
    bool IsValidColor(string input)
    {
        if (!_colors.Contains(input))
        {
            return false;
        }

        return true; // Todos los caracteres son válidos
    }
    bool IsValidOperator(string input)
    {
        if (!_operators.Contains(input))
        {
            return false;
        }

        return true; // Todos los caracteres son válidos
    }
    bool IsValidFunction(string input)
    {
        if (!_NoVoidFunction.Contains(input))
        {
            return false;
        }

        return true; // Todos los caracteres son válidos
    }
    bool IsValidKeyWord(string input)
    {
        if (!_keywords.Contains(input))
        {
            return false;
        }

        return true; // Todos los caracteres son válidos
    }
    private Token ScanToken(TokenType type, string value, int line, int column, EvaluateToken isValid)
    {
        if (isValid(value))
        {
            return new Token(type, value, line, column);

        }
        else
        {
            Exception.Report($"Carácter inesperado en: '{value}' ", _line, _column);
            //_position++;
            return new Token(TokenType.Null, " ", _line, _column);

        }
    }
    #endregion
}