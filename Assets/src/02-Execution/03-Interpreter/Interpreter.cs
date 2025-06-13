using System.Collections.Generic;
public class Interpreter
{
    List<ASTNode> ast;
    public static int rama = 0;
    public RunTimeExceptionHandler RTE = new RunTimeExceptionHandler();
    public Interpreter(List<ASTNode> asT)
    {
        ast = asT;
        rama = 0;
        while (rama < ast.Count)
        {
            try
            {
                Interpretar(rama++);

            }
            catch (RunTimeException e)
            {
                RTE.Report(e.Message, rama, 1);
                break;

            }
        }
        CleanCache();
    }
    public void CleanCache()
    {
        FunctionManager.variables.Clear();
        FunctionManager.labels.Clear();
    }
    public void Interpretar(int rama)
    {
        ast[rama].Execute();
    }

}