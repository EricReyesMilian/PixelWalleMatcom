public interface IFunction
{
    string FunctionName { get; }
    object Execute(params object[] args);
}
