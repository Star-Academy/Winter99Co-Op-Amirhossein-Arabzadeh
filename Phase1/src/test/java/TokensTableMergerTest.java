import org.junit.Assert;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;

public class TokensTableMergerTest {

    @Test
    public void createHashTableOfWordsFromSortedList() {
        MyIndexController myIndexController = new MyIndexController();
        myIndexController.processDocs("amir");

        Merger merger = new TokensTableMerger();
        List<DocsWordOccurrence> testDocsWordOccurrences = new ArrayList<>();
        testDocsWordOccurrences.add(new DocsWordOccurrence("amirhossein", "amir"));
        testDocsWordOccurrences.add(new DocsWordOccurrence("arabzadeh", "amir"));
        testDocsWordOccurrences.add(new DocsWordOccurrence("last", "amir"));
        testDocsWordOccurrences.add(new DocsWordOccurrence("amirhossein", "last"));
        testDocsWordOccurrences.add(new DocsWordOccurrence("arabzadeh", "last"));

        Map<String, List<String>> table = new HashMap<>();
        List<String> amirhosseinDocs = new ArrayList<>();
        amirhosseinDocs.add("amir");
        amirhosseinDocs.add("last");
        List<String> arabzadehDocs = new ArrayList<>();
        arabzadehDocs.add("amir");
        arabzadehDocs.add("last");
        List<String> lastDocs = new ArrayList<>();
        lastDocs.add("amir");

        table.put("amirhossein", amirhosseinDocs);
        table.put("arabzadeh", arabzadehDocs);
        table.put("last", lastDocs);

        Assert.assertEquals(table, merger.createHashTableOfWordsFromSortedList(testDocsWordOccurrences));
    }
}