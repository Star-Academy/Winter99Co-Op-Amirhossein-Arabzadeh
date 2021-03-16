using System;
using Nest;

namespace Phase10Library
{
    public class IndexDefiner
    {
        private readonly IElasticClient _client;

        public IndexDefiner()
        {
            var elasticClientFactory = new ElasticClientFactory();
            _client = elasticClientFactory.CreateElasticClient("http://localhost:9200");
        }

        public void CreateIndex(string index)
        {
            var response = _client.Indices.Create(index, s => s
                .Settings(CreateSettings)
                .Map<Doc>(CreateMapping));
            Console.WriteLine(response.ServerError);
        }

        private IPromise<IIndexSettings> CreateSettings(IndexSettingsDescriptor settingsDescriptor)
        {
            return settingsDescriptor
                .Setting("max_ngram_diff", 7)
                .Analysis(CreateAnalysis);
        }

        private ITypeMapping CreateMapping(TypeMappingDescriptor<Doc> mappingDescriptor)
        {
            return mappingDescriptor
                .Properties(pr => pr
                    .AddNameFieldMapping()
                    .AddContentFieldMapping());
        }

        private IAnalysis CreateAnalysis(AnalysisDescriptor analysisDescriptor)
        {
            return analysisDescriptor
                .TokenFilters(CreateTokenFilters)
                .Analyzers(CreateAnalyzers);
        }

        private static IPromise<IAnalyzers> CreateAnalyzers(AnalyzersDescriptor analyzersDescriptor)
        {
            return analyzersDescriptor
                .Custom(Analyzers.NgramAnalyzer, custom => custom
                    .Tokenizer("standard")
                    .Filters(TokenFilters.LowerCase, TokenFilters.WordDelimiter, TokenFilters.EnglishStopWords, TokenFilters.NgramFilter));
        }

        private static IPromise<ITokenFilters> CreateTokenFilters(TokenFiltersDescriptor tokenFiltersDescriptor)
        {
            return tokenFiltersDescriptor
                .NGram(TokenFilters.NgramFilter, ng => ng
                    .MinGram(3)
                    .MaxGram(10));
        }
    }
}