import java.util.*;

public class HashInvertedIndex implements InvertedIndex{

    private List<String> result = new ArrayList<>();

    private IndexController indexController;
    private HashMap<String, List<String>> table;
    private ListOperator listOperator;

    public HashInvertedIndex(IndexController myIndexController) {
        indexController = myIndexController;
        table = indexController.getInvertedIndexTable();
    }

    public List<String> prepareResultSet(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords, ListOperator listOperator) {
        this.listOperator = listOperator;

        result = initiateResultSetWithDocsContainingFirstUnsignedWords(unSignedInputWords);
        result = listOperator.intersectUnsignedWordsContainingDocs(result, unSignedInputWords);
        result = listOperator.removeDocsWithoutPlusWords(plusSignedInputWords, result);
        result = listOperator.removeDocsContainingMinusSignedWords(minusSignedInputWords, result);
        return result;

    }



    private List<String> initiateResultSetWithDocsContainingFirstUnsignedWords(List<String> unSignedInputWords) {
        List<String> result = new ArrayList<>();
        if (isThereAnyUnsignedWord(unSignedInputWords)) {
            List<String> firstUnSignedInputWordContainingDocs = table.get(unSignedInputWords.get(0));
            result.addAll(firstUnSignedInputWordContainingDocs);
        }
        return result;
    }

    private boolean isThereAnyUnsignedWord(List<String> unSignedInputWords) {
        return unSignedInputWords.size() != 0 && table.containsKey(unSignedInputWords.get(0));
    }


}
