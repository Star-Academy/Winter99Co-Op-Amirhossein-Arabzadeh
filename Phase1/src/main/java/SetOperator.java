import java.util.*;

public class SetOperator implements SetOperate {
    SetCalculate setCalculator = new SetCalculator();

    public List<String> addUnSignedWordsContainingDocsToResult(List<String> result, List<String> unSignedWords, Map<String, List<String>> table) {
        List<String> tempResult = new ArrayList<>(result);
        for (String term : unSignedWords) {
            List<String> listOfDocsContainingUnsignedWord = table.get(term);
            Set<String> setOfDocsContainingUnsignedWord = new HashSet<>(listOfDocsContainingUnsignedWord);
            tempResult = setCalculator.andResultSet(setOfDocsContainingUnsignedWord, result);
        }
        return tempResult;
    }

    public List<String> andResultWithSetOfDocsContainingPlusSignedWords(List<String> plusSignedWords, List<String> result, Map<String, List<String>> table) {
        Set<String> docsWitchHasPlusWords;
        docsWitchHasPlusWords = setCalculator.createSetOfDifferentModeledInputs(plusSignedWords, table);

        //clean the result of docs which have not at least one of the plus sugned words

        return setCalculator.andResultSet(docsWitchHasPlusWords, result);
    }


    public List<String> removeMinusSignedContainingDocsFromResult(List<String> minusSignedWords, List<String> result, Map<String, List<String>> table) {

        Set<String> docsWitchHasMinusWords;
        docsWitchHasMinusWords = setCalculator.createSetOfDifferentModeledInputs(minusSignedWords, table);
        return setCalculator.minusResultSet(docsWitchHasMinusWords, result);

    }


}
