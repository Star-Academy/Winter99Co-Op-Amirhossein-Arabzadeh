using Nest;

namespace Phase10Library
{
    internal static class FieldsDefiner
    {
        public static PropertiesDescriptor<Doc> AddIdFieldMapping(this PropertiesDescriptor<Doc> propertiesDescriptor)
        {
            return propertiesDescriptor
                .Keyword(t => t
                    .Name(n => n.Name));
        }
        
        public static PropertiesDescriptor<Doc> AddContentFieldMapping(this PropertiesDescriptor<Doc> propertiesDescriptor)
        {
            return propertiesDescriptor
                .Text(t => t
                    .Name(n => n.Content)
                    .Fields(f => f
                        .Text(t=>t
                            .Name(n => n.Content)
                            .SetAnalyzer())
                        .SetKeyWord()));
        }
        private static ITextProperty SetAnalyzer(this TextPropertyDescriptor<Doc> selector)
        {
            return selector
                .Analyzer(Analyzers.NgramAnalyzer);
            
        }
        private static PropertiesDescriptor<Doc> SetKeyWord(this PropertiesDescriptor<Doc> selector)
        {
            return selector
                .Keyword(ng => ng
                    .Name("keyword"));
        }
    }
}