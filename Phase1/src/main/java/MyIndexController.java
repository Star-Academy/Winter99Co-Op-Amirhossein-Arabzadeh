import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

public class MyIndexController implements IndexController{
    private List<DocsWordOccurrence> myTokens = new ArrayList<>();
    private static HashMap<String, List<String>> table = new HashMap<>();
    private String folderName;

    public void processDocs(String folderName) {
        this.folderName = folderName;
        myTokens = tokenizeContentsOfDocs();

        Collections.sort(this.myTokens);

        //iterate the tokensArray to find the identical words to merge them
        Merger myMerger = new TokensTableMerger();
        table = myMerger.createHashTableOfWordsFromSortedList(myTokens);
    }

    private List<DocsWordOccurrence> tokenizeContentsOfDocs() {
        docsFileReader tokenizedDocsFileReader = new TokenizingDocsFileReader();
        return tokenizedDocsFileReader.readFiles(folderName);
    }

    public static HashMap<String, List<String>> getInvertedIndexTable() {
        return table;
    }
}
