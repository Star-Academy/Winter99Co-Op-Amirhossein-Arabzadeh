import java.util.*;

public class ArrayListOperator implements ListOperator {

    private ListCalculator listCalculator = new IteratingListCalculator();

    public List<String> addUnSignedWordsContainingDocsToResult(List<String> result, List<String> unSignedWords, Map<String, List<String>> table) {
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

    public List<String> removeDocsWithoutPlusWords(List<String> plusSignedWords, List<String> result, Map<String, List<String>> table) {
        Set<String> docsContainingPlusWords;
        docsContainingPlusWords = listCalculator.createSetOfDifferentModeledInputs(plusSignedWords, table);

        //clean the result of docs which have not at least one of the plus sugned words

        return listCalculator.andResultSet(docsContainingPlusWords, result);
    }


    public List<String> removeMinus(List<String> minusSignedWords, List<String> result, Map<String, List<String>> table) {

        Set<String> docsWitchHasMinusWords;
        docsWitchHasMinusWords = listCalculator.createSetOfDifferentModeledInputs(minusSignedWords, table);
        return listCalculator.minusResultSet(docsWitchHasMinusWords, result);

    }


}
