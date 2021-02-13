import java.util.*;

public class HashInvertedIndex implements InvertedIndex{

    private List<String> result = new ArrayList<>();

    private HashMap<String, List<String>> table;

    private List<String> unSignedWords;
    private List<String> plusSignedWords;
    private List<String> minusSignedWords;

    public HashInvertedIndex(List<String> unSignedWords, List<String> plusSignedWords, List<String> minusSignedWords) {
        this.unSignedWords = unSignedWords;
        this.plusSignedWords = plusSignedWords;
        this.minusSignedWords = minusSignedWords;
    }

    public List<String> prepareResultSet() {
        ListOperator listOperator = new ArrayListOperator();

        table = InvertedIndexController.getInvertedIndexTable();
        result = initiateResultSetWithDocsContainingFirstUnsignedWords();
        result = listOperator.addUnSignedWordsContainingDocsToResult(result, unSignedWords, table);
        result = listOperator.andResultWithSetOfDocsContainingPlusSignedWords(plusSignedWords, result, table);
        result = listOperator.removeMinus(minusSignedWords, result, table);
        return result;

    }



    private List<String> initiateResultSetWithDocsContainingFirstUnsignedWords() {
        List<String> result = new ArrayList<>();
        if (isThereAnyUnsignedWord()) {
            List<String> firstUnSignedInputWordContainingDocs = table.get(unSignedWords.get(0));
            result.addAll(firstUnSignedInputWordContainingDocs);
        }
        return result;
    }

    private boolean isThereAnyUnsignedWord() {
        return unSignedWords.size() != 0 && table.containsKey(unSignedWords.get(0));
    }


}
