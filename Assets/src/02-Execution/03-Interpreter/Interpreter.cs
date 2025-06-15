using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
            //necesito que aqui se espere una cantidad de tiemppo antes de ejecutar la siguiente instruccion
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