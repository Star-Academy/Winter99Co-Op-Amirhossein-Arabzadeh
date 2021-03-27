using System;
using System.Linq;
using Nest;

namespace Phase10Library
{
    public class IndexDefiner : IIndexDefiner
    {
        private readonly IElasticClient _client;
        private readonly ElasticResponseValidator _elasticResponseValidator;
        private const int MaxNgramDiff = 7;
        private const int MinGram = 3;
        private const int MaxGram = 10;

        public IndexDefiner(IElasticClient client, ElasticResponseValidator elasticResponseValidator)
        {
            _client = client;
            _elasticResponseValidator = elasticResponseValidator;
        }

        public void CreateIndex(string index)
        {
            ValidateIndexName(index);
            var response = _client.Indices.Create(index, s => s
                .Settings(CreateSettings)
                .Map<Doc>(CreateMapping));
            Console.WriteLine(response.ServerError);
            _elasticResponseValidator.Validate(response);
        }

        private void ValidateIndexName(string index)
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentException("Provided index name is either null or empty");
            }
            if (index.Any(char.IsUpper))
            {
                throw new ArgumentException("Provided index name has an uppercase character");
            }
        }

        private IPromise<IIndexSettings> CreateSettings(IndexSettingsDescriptor settingsDescriptor)
        {
            return settingsDescriptor
                .Setting(KeyWords.MaxNgramDiff, MaxNgramDiff)
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
                    .Tokenizer(KeyWords.Standard)
                    .Filters(TokenFilters.LowerCase, TokenFilters.WordDelimiter, TokenFilters.EnglishStopWords, TokenFilters.NgramFilter));
        }

        private static IPromise<ITokenFilters> CreateTokenFilters(TokenFiltersDescriptor tokenFiltersDescriptor)
        {
            return tokenFiltersDescriptor
                .NGram(TokenFilters.NgramFilter, ng => ng
                    .MinGram(MinGram)
                    .MaxGram(MaxGram));
        }
    }
}