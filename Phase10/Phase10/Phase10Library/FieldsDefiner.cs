using Nest;

namespace Phase10Library
{
    internal static class FieldsDefiner
    {
        public static PropertiesDescriptor<Doc> AddNameFieldMapping(this PropertiesDescriptor<Doc> propertiesDescriptor)
        {
            return propertiesDescriptor
                .Number(t => t
                    .Type(NumberType.Long)
                    .Name(n => n.Name));
        }
        
        public static PropertiesDescriptor<Doc> AddContentFieldMapping(this PropertiesDescriptor<Doc> propertiesDescriptor,
            Settings settings)
        {
            return propertiesDescriptor
                .Text(t => t
                    .Name(n => n.Content)
                    .Fields(f => f
                        .Text(tpd=>tpd
                            .Name(n => n.Content)
                            .SetAnalyzer(settings))
                        .SetKeyWord(settings)));
        }
        private static ITextProperty SetAnalyzer(this TextPropertyDescriptor<Doc> selector, Settings settings)
        {
            return selector
                .Analyzer(settings.Analyzers.NgramAnalyzer);
            
        }
        private static PropertiesDescriptor<Doc> SetKeyWord(this PropertiesDescriptor<Doc> selector, Settings settings)
        {
            return selector
                .Keyword(ng => ng
                    .Name(settings.KeyWords.KeyWord));
        }
    }
}