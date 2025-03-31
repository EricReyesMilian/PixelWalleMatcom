// Ejemplo de uso en Unity
using UnityEngine;
using System.Collections.Generic;
public class LexerTest : MonoBehaviour
{
    void Start()
    {
        string code = @"
            Spawn(0, 0)
            Color(Black)
            n <- 5
            k <- 3 + 3 * 10
            n <- k * 2
            actual-x <- GetActualX()
            i <- 0

            loop-1
            DrawLine(1, 0, 1)
            i <- i + 1
            is-brush-color-blue <- IsBrushColor(Blue)
            Goto [loop-ends-here] (is-brush-color-blue == 1)
            GoTo [loop1] (i < 10)

            Color(Blue)
            GoTo [loop1] (1 == 1)

            loop-ends-here
            
        ";

        Lexer lexer = new Lexer(code);
        List<Token> tokens = lexer.Tokenize();

        foreach (Token token in tokens)
        {
            Debug.Log(token);
        }
    }
}