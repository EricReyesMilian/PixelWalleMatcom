using System.Collections.Generic;
using System;
using UnityEngine;

public class Lexer
{
    private string _input;
    private int _position;
    private int _line = 1;
    private int _column = 1;

    // Palabras clave
    private readonly HashSet<string> _keywords = new() { "si", "mientras", "funcion", "retornar", "dibujar" };

    // Operadores
    private readonly HashSet<string> _operators = new() { "+", "-", "*", "**", "%", "/", "=", "<", ">", "<=", ">=", "==", "<-", "&&", "||" };
    // Delimitadores
    private readonly HashSet<char> _punctuation = new() { '(', ')', ',', '[', ']' };

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
    }
    public List<Token> Tokenize()
    {
        List<Token> tokens = new();
        Token token;


        do
        {
            token = GetNextToken();
            tokens.Add(token);
        } while (token.Type != TokenType.END);

        return tokens;
    }
    private Token? GetNextToken()
    {
        SkipWhitespace();
        if (_position >= _input.Length)
            return new Token(TokenType.END, "", _line, _column);

        char current = _input[_position];

        // Máquina de Estados Finitos
        if (char.IsDigit(current))
            return ReadNumber();

        if (char.IsLetter(current) || current == '_')
            return ReadIdentifier();
        if (_operators.Contains(current.ToString()))
            return ReadOperator();

        if (_punctuation.Contains(current))
            return ReadPunctuation();

        // Carácter no reconocido
        ErrorHandler.errorHandler.Error($"Carácter inesperado: '{current}' {_line}, {_column}");
        return null;

    }
    private Token ReadOperator()
    {
        char op = _input[_position];
        int currentLine = _line;
        int currentCol = _column;
        Advance();

        // Operadores de dos caracteres (==, <=, >=, etc.)
        if (_position < _input.Length && _operators.Contains(_input[_position].ToString()))
        {
            string doubleOp = op.ToString() + _input[_position];
            if (IsValidDoubleOperator(doubleOp))
            {
                Advance();
                return new Token(TokenType.Operator, doubleOp, currentLine, currentCol);
            }
        }

        return new Token(TokenType.Operator, op.ToString(), currentLine, currentCol);
    }
    private Token ReadNumber()
    {
        int start = _position;
        int startLine = _line;
        int startCol = _column;

        while (_position < _input.Length && (char.IsDigit(_input[_position])))
        {
            Advance();
        }

        string value = _input.Substring(start, _position - start);
        return new Token(TokenType.Number, value, startLine, startCol);
    }
    private Token ReadIdentifier()
    {
        int start = _position;
        int startLine = _line;
        int startCol = _column;

        while (_position < _input.Length && (char.IsLetterOrDigit(_input[_position]) || _input[_position] == '_' || _input[_position] == '-'))
        {
            Advance();
        }

        string value = _input.Substring(start, _position - start);
        TokenType type;
        if (_keywords.Contains(value))
        {
            type = TokenType.Keyword;
        }
        else
        {
            type = TokenType.Identifier;
        }
        return new Token(type, value, startLine, startCol);
    }
    private Token ReadPunctuation()
    {
        char punct = _input[_position];
        int currentLine = _line;
        int currentCol = _column;
        Advance();
        return new Token(TokenType.Punctuation, punct.ToString(), currentLine, currentCol);
    }
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
        while (_position < _input.Length && char.IsWhiteSpace(_input[_position]))
        {
            Advance();
        }
    }
    private bool IsValidDoubleOperator(string op)
    {
        return op == "==" || op == "<-" || op == "<=" || op == ">=" || op == "&&" || op == "||" || op == "**";
    }
    #endregion
}