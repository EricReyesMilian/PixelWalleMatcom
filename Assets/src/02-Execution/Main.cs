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

        Parser parser = new Parser(tokens);
        List<ASTNode> ast = parser.Parse();
        foreach (var rama in ast)
        {
            rama.PrintTree();
        }
        SemanticCheck semanticCheck = new SemanticCheck(ast);
        semanticCheck.Analyze();
        if (!parser.sintacticException.HasErrors && !semanticCheck.error.HasErrors && !lexer.Exception.HasErrors)
        {
            Interpreter interpreter = new Interpreter(ast);

        }
        ErrorHandler.errorHandler.Error("\n\n>>\n");
        ErrorHandler.errorHandler.Show();

    }
}
