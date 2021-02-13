import java.io.IOException;
import java.util.*;

public class ControllerImpl implements Controller {
    private List<MyToken> myTokens = new ArrayList<>();
    private static HashMap<String, List<String>> table = new HashMap<>();
    private InvertedIndex invertedIndex = new HashedInvertedIndex();


    @Override
    public List<String> getResult(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {
        return invertedIndex.prepareResult(plusSignedInputWords, minusSignedInputWords, unSignedInputWords);
    }

    public void processDocs() {
        myTokens = tokenizeContentsOfDocs();

        Collections.sort(this.myTokens);

        //iterate the tokensArray to find the identical words to merge them
        mergeIdenticalWordsAndCreateHashTableOfWords();
    }

    public List<MyToken> tokenizeContentsOfDocs() {
        MyFileReader tokenizedMyFileReader = new TokenizerMyFileReader();
        return tokenizedMyFileReader.readFiles();
    }
    public void mergeIdenticalWordsAndCreateHashTableOfWords() {
        for (int i = 0; i < (myTokens.size() -1); i++) {
            Set<String> docs = new HashSet<>();
            while (i<(myTokens.size() - 2) && myTokens.get(i).getTerm().equals(myTokens.get(i+1).getTerm())) {
                docs.add(myTokens.get(i).getDoc());
                i++;
            }
            docs.add(myTokens.get(i).getDoc());
            table.put(myTokens.get(i).getTerm(), new ArrayList<>(docs));
            i++;
        }
    }



    public static HashMap<String, List<String>> getInvertedIndexTable() {
        return table;
    }

    public void addToken(MyToken myToken) {
        myTokens.add(myToken);
    }

}
