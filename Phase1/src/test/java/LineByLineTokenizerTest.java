import org.junit.Assert;
import org.junit.Test;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.*;

public class LineByLineTokenizerTest {

    @Test
    public void tokenizeOneDoc() {
        Tokenizer tokenizer = new LineByLineTokenizer();
        List<WordOccurrence> wordOccurrences = new ArrayList<>();
        wordOccurrences.add(new DocsWordOccurrence("amirhossein", "amir"));
        Assert.assertEquals(wordOccurrences.get(0), tokenizer.tokenizeOneDoc(new File("amir"), "amir").get(0));
    }
}