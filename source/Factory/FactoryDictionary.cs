using System.Collections.Generic;

namespace SimpleCalculator.source.Factory
{
    public static class FactoryDictionaryProvider
    {
        public static readonly Dictionary<string, IFactory> FactoryDictionary = new Dictionary<string, IFactory>
        {
            {"add", new AdditionFactory()},
            {"sbu", new SubtractionFactory()},
            {"mul", new MultiplicationFactory()},
            {"sqrt", new SquareRootFactory()}
        };
    }
}