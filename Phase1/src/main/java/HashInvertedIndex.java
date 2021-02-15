import java.util.*;

public class HashInvertedIndex implements InvertedIndex{

    private List<String> result = new ArrayList<>();

    private HashMap<String, List<String>> table;

    public List<String> prepareResultSet(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {
        ListOperator listOperator = new ArrayListOperator();

        table = MyIndexController.getInvertedIndexTable();
        result = initiateResultSetWithDocsContainingFirstUnsignedWords(unSignedInputWords);
        result = listOperator.addUnSignedWordsContainingDocsToResult(result, unSignedInputWords, table);
        result = listOperator.removeDocsWithoutPlusWords(plusSignedInputWords, result, table);
        result = listOperator.removeMinus(minusSignedInputWords, result, table);
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
