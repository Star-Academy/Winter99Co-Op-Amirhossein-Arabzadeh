import org.junit.Assert;
import org.junit.Test;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;

import static org.junit.Assert.*;

public class TokenizingDocsFileReaderTest {

    @Test
    public void readFiles() {
        TokenizingDocsFileReader tokenizingDocsFileReader = new TokenizingDocsFileReader();

        List<DocsWordOccurrence> expectedDocsWordOccurrences = new ArrayList<>();
        expectedDocsWordOccurrences.add(new DocsWordOccurrence("amirhossein", "amir"));
        expectedDocsWordOccurrences.add(new DocsWordOccurrence("arabzadeh", "amir"));
        expectedDocsWordOccurrences.add(new DocsWordOccurrence("last", "amir"));
        expectedDocsWordOccurrences.add(new DocsWordOccurrence("amirhossein", "last"));
        expectedDocsWordOccurrences.add(new DocsWordOccurrence("arabzadeh", "last"));

        MyIndexController myIndexController = new MyIndexController();
        myIndexController.processDocs("amir");

        Tokenizer tokenizer = new LineByLineTokenizer();
        List<DocsWordOccurrence> testResult = tokenizingDocsFileReader.readFiles("amir", tokenizer);

        Collections.sort(testResult);
        Collections.sort(expectedDocsWordOccurrences);
        for (int i = 0; i<expectedDocsWordOccurrences.size(); i++) {
            Assert.assertEquals(expectedDocsWordOccurrences.get(i), testResult.get(i));
        }
    }
}