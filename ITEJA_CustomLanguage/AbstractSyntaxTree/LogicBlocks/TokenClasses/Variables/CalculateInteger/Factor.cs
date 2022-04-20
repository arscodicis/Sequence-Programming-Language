namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables.CalculateInteger
{
    /// <summary>
    /// Single numeric value
    /// </summary>
    public class Factor
    {
        public double Value { get; }
        public Factor(double pValue)
        {
            Value = pValue;
        }
    }
}
