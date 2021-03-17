﻿using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class IndexDefinerTest
    {
        [Fact]
        public void CreateIndex_ShouldCreateIndexMatchingByWantedElasticQuery_WhenParameterIsValid()
        {
            var uri = new Uri(Addresses.HttpLocalhost);
            IElasticClient client = new ElasticClient(uri);
            var indexDefiner = new IndexDefiner(client);
            indexDefiner.CreateIndex("my_index");
            var response = client.LowLevel.Indices.GetMapping<StringResponse>("my_index");
            var expectedMapping =
                "{\n  \"my_index\" : {\n" +
                "    \"mappings\" : {\n " +
                "     \"properties\" : {\n" +
                "        \"content\" : {\n" +
                "          \"type\" : \"text\",\n" +
                "          \"fields\" : {\n" +
                "            \"content\" : {\n" +
                "              \"type\" : \"text\",\n" +
                "              \"analyzer\" : \"my-ngram-analyzer\"\n" +
                "            },\n            \"keyword\" : {\n" +
                "              \"type\" : \"keyword\"\n" +
                "            }\n          }\n        },\n        \"name\" : {\n" +
                "          \"type\" : \"long\"\n" +
                "        }\n      }\n    }\n  }\n}";
            var trimmedExpectedMapping = string.Concat(expectedMapping.Where(c => !Char.IsWhiteSpace(c)));
            string trimmedResponseBody = string.Concat(response.Body.Where(c => !Char.IsWhiteSpace(c)));
            Assert.Equal(trimmedResponseBody, trimmedExpectedMapping);
        }
        
        public static IEnumerable<object[]> CreateIndexInvalidArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {"    "},
            new object[] {"SomeStringWithCapitalCharacter"},
        };
        [Theory]
        [MemberData(nameof(CreateIndexInvalidArguments))]
        public void CreateIndex_ShouldThrowArgumentException_WhenParametersAreInvalid(string indexName)
        {
            Uri url = new Uri(Addresses.HttpLocalhost);
            IElasticClient client = new ElasticClient(url);
            IndexDefiner indexDefiner = new IndexDefiner(client);
            void Action() => indexDefiner.CreateIndex(indexName);
            Assert.Throws<ArgumentException>(Action);
        }
    }
}