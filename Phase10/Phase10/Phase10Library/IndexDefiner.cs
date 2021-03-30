using System;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using Nest;

namespace Phase10Library
{
    public class IndexDefiner : IIndexDefiner
    {
        private const string LogfileTxt = "logfile.txt";

        private readonly IElasticClient _client;
        private readonly ElasticResponseValidator _elasticResponseValidator;
        private readonly Settings _settings;
        private const int MaxNgramDiff = 7;
        private const int MinGram = 3;
        private const int MaxGram = 10;

        public IndexDefiner(IElasticClient client, ElasticResponseValidator elasticResponseValidator, Settings settings)
        {
            _client = client;
            _elasticResponseValidator = elasticResponseValidator;
            _settings = settings;
        }

        public void CreateIndex(string index)
        {
            ValidateIndexName(index);
            var response = _client.Indices.Create(index, s => s
                .Settings(CreateSettings)
                .Map<Doc>(CreateMapping));
            if (response.ServerError is not null)
            {
                LogException(response.ServerError);    
            }
            _elasticResponseValidator.Validate(response);
        }
        
        private void LogException(ServerError serverError)
        {
            StreamWriter log;
            
            if (!File.Exists(LogfileTxt))
            {
                log = new StreamWriter(LogfileTxt);
            }
            else
            {
                log = File.AppendText(LogfileTxt);
            }

            log.WriteLine("Exception happened in FileReader.GetFileContent: " + serverError.Error);
            log.Close();
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
                    .AddContentFieldMapping(_settings));
        }

        private IAnalysis CreateAnalysis(AnalysisDescriptor analysisDescriptor)
        {
            return analysisDescriptor
                .TokenFilters(CreateTokenFilters)
                .Analyzers(CreateAnalyzers);
        }

        private IPromise<IAnalyzers> CreateAnalyzers(AnalyzersDescriptor analyzersDescriptor)
        {
            return analyzersDescriptor
                .Custom(Analyzers.NgramAnalyzer, custom => custom
                    .Tokenizer(KeyWords.Standard)
                    .Filters(TokenFilters.LowerCase,
                        TokenFilters.WordDelimiter,
                        TokenFilters.EnglishStopWords,
                        TokenFilters.NgramFilter));
        }

        private IPromise<ITokenFilters> CreateTokenFilters(TokenFiltersDescriptor tokenFiltersDescriptor)
        {
            return tokenFiltersDescriptor
                .NGram(TokenFilters.NgramFilter, ng => ng
                    .MinGram(MinGram)
                    .MaxGram(MaxGram));
        }
    }
}