import java.util.*;

public class MySearchController implements SearchController {
    private InvertedIndex invertedIndex;


    @Override
    public List<String> getSetOfDocsForUser(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {
        invertedIndex = new HashInvertedIndex();
        return invertedIndex.prepareResultSet(plusSignedInputWords, minusSignedInputWords, unSignedInputWords);
    }



}
