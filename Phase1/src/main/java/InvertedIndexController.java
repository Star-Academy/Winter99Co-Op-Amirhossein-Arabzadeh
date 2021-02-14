import java.util.*;

public class InvertedIndexController implements Controller {
    private List<MyToken> myTokens = new ArrayList<>();
    private static HashMap<String, List<String>> table = new HashMap<>();
    private InvertedIndex invertedIndex;
    private String folderName;


    @Override
    public List<String> getResult(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {
        invertedIndex = new HashInvertedIndex(unSignedInputWords, plusSignedInputWords, minusSignedInputWords);
        return invertedIndex.prepareResultSet();
    }

    public void processDocs(String folderName) {
        this.folderName = folderName;
        myTokens = tokenizeContentsOfDocs();

        Collections.sort(this.myTokens);

        //iterate the tokensArray to find the identical words to merge them
        Merger myMerger = new TokensTableMerger();
        table = myMerger.createHashTableOfWords(myTokens);
    }

    private List<MyToken> tokenizeContentsOfDocs() {
        MyFileReader tokenizedMyFileReader = new TokenizingMyFileReader();
        return tokenizedMyFileReader.readFiles(folderName);
    }

    public static HashMap<String, List<String>> getInvertedIndexTable() {
        return table;
    }

}
