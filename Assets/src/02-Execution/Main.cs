using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class Main : MonoBehaviour
{
    public TMP_InputField inputField;
    public string codeRaw;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCode()
    {
        codeRaw = inputField.text;
    }
    public void Run()
    {
        FunctionManager.variables.Clear();
        FunctionManager.labels.Clear();
        CanvasGrid.Reset();

        Lexer lexer = new Lexer(codeRaw);
        List<Token> tokens = lexer.Tokenize();
        if (!lexer.Exception.HasErrors)
        {
            Parser parser = new Parser(tokens);
            List<ASTNode> ast = parser.Parse();
            foreach (var rama in ast)
            {
                rama.PrintTree();
            }
            Interpreter interpreter = new Interpreter(ast);

        }
    }
}
