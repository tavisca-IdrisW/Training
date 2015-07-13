using System.ServiceModel;

namespace DemoServer
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        double AddNumbers(double number1, double number2);
        [OperationContract]
        double SubstractNumbers(double number1, double number2);
        [OperationContract]
        double MultiplyNumbers(double number1, double number2);
        [OperationContract]
        double DivisionNumbers(double number1, double number2);
    }
}

