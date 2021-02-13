import java.util.*;

public class HashedInvertedIndex implements InvertedIndex{

    private List<String> result = new ArrayList<>();

    private HashMap<String, List<String>> table;

    private List<String> unSignedWords;
    private List<String> plusSignedWords;
    private List<String> minusSignedWords;

    public HashedInvertedIndex(List<String> unSignedWords, List<String> plusSignedWords, List<String> minusSignedWords) {
        this.unSignedWords = unSignedWords;
        this.plusSignedWords = plusSignedWords;
        this.minusSignedWords = minusSignedWords;
    }

    public List<String> prepareResultSet() {
        SetOperate setOperator = new SetOperator();

        table = ControllerImpl.getInvertedIndexTable();
        result = initiateResultSetWithDocsContainingFirstUnsignedWords();
        result = setOperator.addUnSignedWordsContainingDocsToResult(result, unSignedWords, table);
        result = setOperator.andResultWithSetOfDocsContainingPlusSignedWords(plusSignedWords, result, table);
        result = setOperator.removeMinusSignedContainingDocsFromResult(minusSignedWords, result, table);
        return result;

    }



    private List<String> initiateResultSetWithDocsContainingFirstUnsignedWords() {
        List<String> result = new ArrayList<>();
        if (table.containsKey(unSignedWords.get(0))) {
            List<String> firstUnSignedInputWordContainingDocs = table.get(unSignedWords.get(0));
            result.addAll(firstUnSignedInputWordContainingDocs);
        }
        return result;
    }




}
