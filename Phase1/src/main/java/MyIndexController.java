import java.util.*;

public class MyIndexController implements IndexController{
    private List<DocsWordOccurrence> myTokens = new ArrayList<>();
    private static HashMap<String, List<String>> table = new HashMap<>();
    public void processDocs(String folderName) {
        table = getTableOfWordsAsKeyAndDocsAsValue(folderName);
    }
    private HashMap<String, List<String>> getTableOfWordsAsKeyAndDocsAsValue(String folderName) {
        myTokens = tokenizeContentsOfDocs(folderName);
        //iterate the tokensArray to find the identical words to merge them
        Merger myMerger = new TokensTableMerger();
        return myMerger.createHashTableOfWordsFromSortedList(myTokens);
    }

    private List<DocsWordOccurrence> tokenizeContentsOfDocs(String folderName) {
        DocsFileReader tokenizedDocsFileReader = new TokenizingDocsFileReader();
        return tokenizedDocsFileReader.readFiles(folderName);
    }

    public HashMap<String, List<String>> getInvertedIndexTable() {
        return table;
    }
}
