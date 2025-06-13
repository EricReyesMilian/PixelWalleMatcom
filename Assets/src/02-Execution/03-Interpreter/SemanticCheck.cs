using System.Collections.Generic;
using System;
public class SemanticCheck
{
    private readonly List<ASTNode> _ast;
    public SemanticExceptionHandler error = new SemanticExceptionHandler();
    public static Dictionary<string, int> variables = new Dictionary<string, int>();
    public static Dictionary<string, int> labels = new Dictionary<string, int>();

    public SemanticCheck(List<ASTNode> ast)
    {
        variables.Clear();
        labels.Clear();
        _ast = ast;
    }

    public void Analyze()
    {
        foreach (var node in _ast)
        {
            VisitLineStart(node);
        }

    }

    private void VisitLineStart(ASTNode node)
    {
        switch (node)
        {
            case FunctionNode f:
                if (!FunctionManager.IsIntFunction(f))
                {
                    CheckFunction(f);
                }
                else
                {
                    error.Report($"La funcion {f.token.Value} no representa una instruccion", f.token.Line, f.token.Column);

                }
                break;
            case AssingNode a:
                CheckAssing(a);
                break;
            case LoopNode l:
                CheckLoop(l);
                break;
            case LabelNode label:
                SaveLabel(label);
                break;
            default:
                error.Report($"El {node.token.Value} no es el inicio de una instruccion valida", node.token.Line, node.token.Column);
                break;
        }
    }
    //falta
    public void CheckFunction(FunctionNode functionNode)
    {
        for (int i = 0; i < functionNode.param.Count; i++)
        {
            ASTNode node = functionNode.param[i];

            switch (node)
            {
                case FunctionNode f:
                    if (FunctionManager.IsIntFunction(f))
                    {
                        CheckFunction(f);
                    }
                    else
                    {
                        error.Report($"La funcion {node.token.Value} asignada en el parametro de la funcion ${functionNode.token.Value} es de retorno vacio", node.token.Line, node.token.Column);

                    }
                    break;
                case BinaryOperatorNode n: //revisar que esto no se parta con goto
                    CheckBinary(n);
                    if (CheckBool(n))
                    {
                        error.Report($"El valor booleano {node.token.Value} asignada en el parametro de la funcion ${functionNode.token.Value} no es valido", node.token.Line, node.token.Column);

                    }
                    break;
                case ColorNode n:

                    break;
                case VariableNode v:
                    CheckVariable(v);
                    break;
                case ConstantNode n:
                    break;
                default:
                    error.Report($"El parametro {node.token.Value} no es un parametro valido", node.token.Line, node.token.Column);
                    return;


                    // Agrega más casos según tus nodos AST
            }
        }

        if (!functionNode.CheckParam(functionNode.token.Value, ParseParams(functionNode.param)))
        {
            error.Report($"Los Parametros de la funcion {functionNode.token.Value} son incorrectos", functionNode.token.Line, functionNode.token.Column);
        }
    }
    public int[] ParseParams(List<ASTNode> p)
    {
        int[] result = new int[p.Count];
        for (int i = 0; i < p.Count; i++)
        {
            switch (p[i])
            {
                case BinaryOperatorNode n:
                    CheckBinary(n);
                    if (CheckBool(n))
                    {
                        result[i] = 0;//bool
                    }
                    else
                    {
                        result[i] = 1;//number
                    }
                    break;

                case VariableNode v:
                    CheckVariable(v);
                    result[i] = 1;//number
                    break;
                case ConstantNode c:
                    CheckConstant(c);
                    result[i] = 1;//number
                    break;
                case ColorNode o:

                    result[i] = 2;//color
                    break;
                default:
                    error.Report($"La expresion {p[i].token.Value} no es un parametro valido", p[i].token.Line, p[i].token.Column);
                    break;
            }
        }
        return result;
    }
    public void CheckConstant(ConstantNode constantNode)
    {

    }
    public void CheckLoop(LoopNode loopNode)
    {
        CheckLabel(loopNode.label);
        ASTNode expresion = loopNode.condition;
        switch (expresion)
        {
            case BinaryOperatorNode n:
                CheckBinary(n);
                if (!CheckBool(n))
                {
                    error.Report($"La expresion {expresion.token.Value} no es una condicion booleana valida para el GoTo", expresion.token.Line, expresion.token.Column);
                }
                break;

            default:
                error.Report($"La expresion {expresion.token.Value} no es una condicion valida para el GoTo", expresion.token.Line, expresion.token.Column);
                return;
        }
    }
    public bool CheckBool(BinaryOperatorNode binaryNode)
    {
        string op = binaryNode.token.Value;
        ASTNode right = binaryNode.Right;
        ASTNode left = binaryNode.Left;
        if (op == "&&" || op == "||")
        {
            switch (right)
            {
                case BinaryOperatorNode n:
                    CheckBinary(n);
                    if (!CheckBool(n))
                    {
                        error.Report($"La expresion {right.token.Value} no es booleana", right.token.Line, right.token.Column);

                    }
                    break;

                default:
                    error.Report($"La expresion {right.token.Value} no es booleana", right.token.Line, right.token.Column);
                    return false;

            }
            switch (left)
            {
                case BinaryOperatorNode n:
                    CheckBinary(n);
                    if (!CheckBool(n))
                    {
                        error.Report($"La expresion {left.token.Value} no es booleana", left.token.Line, left.token.Column);

                    }
                    break;

                default:
                    error.Report($"La expresion {left.token.Value} no es booleana", left.token.Line, left.token.Column);
                    return false;

            }
        }
        else if (op == "<=" || op == ">=" || op == "==" || op == "<" || op == ">")
        {
            switch (right)
            {
                case BinaryOperatorNode n:
                    CheckBinary(n);
                    if (CheckBool(n))
                    {
                        error.Report($"La expresion {right.token.Value} no puede es booleana para efectuar la comparacion", right.token.Line, right.token.Column);

                    }
                    break;
                case FunctionNode f:
                    if (FunctionManager.IsIntFunction(f))
                    {
                        CheckFunction(f);
                    }
                    else
                    {
                        error.Report($"La funcion {right.token.Value} asignada en la operacion es de retorno vacio", right.token.Line, right.token.Column);

                    }
                    break;

                case VariableNode v:
                    CheckVariable(v);
                    break;
                case ConstantNode c:
                    CheckConstant(c);
                    break;
                default:
                    error.Report($"La expresion {right.token.Value} no es booleana", right.token.Line, right.token.Column);
                    return false;

            }
            switch (left)
            {
                case BinaryOperatorNode n:
                    CheckBinary(n);
                    if (CheckBool(n))
                    {
                        error.Report($"La expresion {left.token.Value} no puede es booleana para efectuar la comparacion", left.token.Line, left.token.Column);

                    }
                    break;
                case FunctionNode f:
                    if (FunctionManager.IsIntFunction(f))
                    {
                        CheckFunction(f);
                    }
                    else
                    {
                        error.Report($"La funcion {left.token.Value} asignada en la operacion es de retorno vacio", left.token.Line, left.token.Column);

                    }
                    break;

                case VariableNode v:
                    CheckVariable(v);
                    break;
                case ConstantNode c:
                    CheckConstant(c);
                    break;
                default:
                    error.Report($"La expresion {left.token.Value} no es booleana", left.token.Line, left.token.Column);
                    return false;

            }
        }
        else
        {
            return false;
        }
        return true;
    }
    public void CheckLabel(LabelNode labelNode)
    {
        if (!labels.ContainsKey(labelNode.token.Value))
        {
            error.Report($"El label {labelNode.token.Value} no existe", labelNode.token.Line, labelNode.token.Column);

        }
    }
    public void SaveLabel(LabelNode labelNode)
    {
        if (labels.ContainsKey(labelNode.token.Value))
        {
            error.Report($"El label {labelNode.token.Value} ya estaba declarado", labelNode.token.Line, labelNode.token.Column);

        }
        else
        {
            labels.Add(labelNode.token.Value, labelNode.token.Line);

        }

    }
    public void CheckAssing(AssingNode assingNode)
    {
        ASTNode right = assingNode.right;
        switch (right)
        {
            case FunctionNode f:
                if (FunctionManager.IsIntFunction(f))
                {
                    CheckFunction(f);
                }
                else
                {
                    error.Report($"La funcion {right.token.Value} asignada en la operacion es de retorno vacio", right.token.Line, right.token.Column);

                }
                break;
            case BinaryOperatorNode n:
                CheckBinary(n);
                if (CheckBool(n))
                {
                    error.Report($"La expresion {n.token.Value} no es de tipo entero por lo que no se puede llevar a cabo la asignacion a la variable", n.token.Line, n.token.Column);
                }
                break;

            case VariableNode v:
                CheckVariable(v);
                break;
            case ConstantNode c:
                CheckConstant(c);
                break;
            default:
                error.Report($"El termino {right.token.Value} no se puede asignar a la variable ${assingNode.token.Value}", right.token.Line, right.token.Column);
                return;
        }
        SaveVariable(assingNode.variable);

    }
    public void CheckBinary(BinaryOperatorNode binaryNode)
    {
        ASTNode right = binaryNode.Right;
        ASTNode left = binaryNode.Left;
        switch (right)
        {
            case FunctionNode f:
                if (FunctionManager.IsIntFunction(f))
                {
                    CheckFunction(f);
                }
                else
                {
                    error.Report($"La funcion {right.token.Value} asignada en la operacion es de retorno vacio", right.token.Line, right.token.Column);

                }
                break;
            case BinaryOperatorNode n:
                CheckBinary(n);
                break;

            case VariableNode v:
                CheckVariable(v);
                break;
            case ConstantNode c:
                CheckConstant(c);
                break;
            default:
                error.Report($"El termino {right.token.Value} no es correcto en la  operacion", right.token.Line, right.token.Column);
                return;
        }
        switch (left)
        {
            case FunctionNode f:
                if (FunctionManager.IsIntFunction(f))
                {
                    CheckFunction(f);
                }
                else
                {
                    error.Report($"La funcion {left.token.Value} asignada en la operacion es de retorno vacio", left.token.Line, left.token.Column);

                }
                break;
            case BinaryOperatorNode n:
                CheckBinary(n);
                break;

            case VariableNode v:
                CheckVariable(v);
                break;
            case ConstantNode c:
                CheckConstant(c);
                break;
            default:
                error.Report($"El termino {left.token.Value} no es un correcto en la  operacion", left.token.Line, left.token.Column);
                return;
        }

    }
    public void CheckVariable(VariableNode variableNode)
    {

        if (!variables.ContainsKey(variableNode.token.Value))
        {
            error.Report($"La variable {variableNode.token.Value} no ha sido declarada", variableNode.token.Line, variableNode.token.Column);

        }

    }
    public void SaveVariable(VariableNode variableNode)
    {
        variables[variableNode.token.Value] = 0;
    }
}