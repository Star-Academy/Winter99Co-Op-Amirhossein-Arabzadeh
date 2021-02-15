import java.util.*;

public class ArrayListOperator implements ListOperator {

    private ListCalculator listCalculator;
    private IndexController indexController;
    private Map<String, List<String>> table;

    public ArrayListOperator(ListCalculator listCalculator, IndexController indexController) {
        this.listCalculator = listCalculator;
        this.indexController = indexController;
        this.table = indexController.getInvertedIndexTable();
    }

    public List<String> intersectUnsignedWordsContainingDocs(List<String> result, List<String> unSignedWords) {
        List<String> tempResult = new ArrayList<>(result);
        for (String term : unSignedWords) {
            if (table.containsKey(term)){
                List<String> listOfDocsContainingUnsignedWord = table.get(term);
                Set<String> setOfDocsContainingUnsignedWord = new HashSet<>(listOfDocsContainingUnsignedWord);
                tempResult = listCalculator.andResultSet(setOfDocsContainingUnsignedWord, result);
            }
        }
        return tempResult;
    }

    public List<String> removeDocsWithoutPlusWords(List<String> plusSignedWords, List<String> result) {
        Set<String> docsContainingPlusWords = listCalculator.createSetOfDifferentModeledInputs(plusSignedWords, table);
        return listCalculator.andResultSet(docsContainingPlusWords, result);
    }


    public List<String> removeDocsContainingMinusSignedWords(List<String> minusSignedWords, List<String> result) {
        Set<String> docsContainingMinusWords = listCalculator.createSetOfDifferentModeledInputs(minusSignedWords, table);
        return listCalculator.minusResultSet(docsContainingMinusWords, result);
    }


}
