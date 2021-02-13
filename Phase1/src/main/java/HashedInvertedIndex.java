import java.util.*;

public class HashedInvertedIndex implements InvertedIndex{

    private List<String> result = new ArrayList<>();

    private HashMap<String, List<String>> table;

    private List<String> unSignedWords;
    private List<String> plusSignedWords;
    private List<String> minusSignedWords;

    public List<String> prepareResult(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {

        this.minusSignedWords = minusSignedInputWords;
        this.plusSignedWords = plusSignedInputWords;
        this.unSignedWords = unSignedInputWords;
        table = ControllerImpl.getInvertedIndexTable();

        //one word search
//        Scanner scanner = new Scanner(System.in);
//        String searchingTerm = scanner.nextLine();
//        if (table.get(searchingTerm.toLowerCase()).size() != 0) {
//            for (String doc : table.get(searchingTerm.toLowerCase())) {
//                System.out.println(doc);
//            }
//        }


        List<String> tempResult;

        initiateResult(this.table);
        tempResult = result;
        result = getNotSignedDocs(table, tempResult);


        result = plusDocs(table);

        result = minusDocs(table);

        return result;

    }



    public void initiateResult(HashMap<String, List<String>> table) {
        if (table.containsKey(unSignedWords.get(0).toLowerCase())) {
            result.addAll(table.get(unSignedWords.get(0).toLowerCase()));
        }
    }





    public List<String> getNotSignedDocs(HashMap<String, List<String>> table, List<String> tempResult) {
        for (String term : unSignedWords) {
            for (String doc : result) {
                if (!table.get(term.toLowerCase()).contains(doc)) {
                    tempResult.remove(doc);
                }
            }
        }
        result = tempResult;
        return result;
    }




    public List<String> plusDocs(HashMap<String, List<String>> table) {
        Set<String> docsWitchHasPlusWords;
        docsWitchHasPlusWords = createSetOfDifferentModeledInputs(table, plusSignedWords);


        //clean the result of docs which have not at least one of the plus sugned words
        result = andResultSet(docsWitchHasPlusWords);

        return result;
    }

    public List<String> andResultSet(Set<String> docsWitchHasPlusWords) {
        ArrayList<String> tempResult;
        tempResult = new ArrayList<>(result);
        for (String term : result) {
            if (!docsWitchHasPlusWords.contains(term)) {
                tempResult.remove(term);
            }
        }
        result = tempResult;
        return result;
    }

    public List<String> minusDocs(HashMap<String, List<String>> table) {

        Set<String> docsWitchHasMinusWords;
        docsWitchHasMinusWords = createSetOfDifferentModeledInputs(table, minusSignedWords);
        result = minusResultSet(docsWitchHasMinusWords);
        return result;

    }

    public List<String> minusResultSet(Set<String> anotherSet) {
        ArrayList<String> tempResult;
        tempResult = new ArrayList<>(result);
        for (String term : result) {
            if (anotherSet.contains(term)) {
                tempResult.remove(term);
            }
        }
        result = tempResult;
        return result;
    }

    public Set<String> createSetOfDifferentModeledInputs(HashMap<String, List<String>> table, List<String> partition) {
        Set<String> docsWitchHasMinusWords = new HashSet<>();
        for (String term : partition) {
            if (table.containsKey(term.toLowerCase())) {
                docsWitchHasMinusWords.addAll(table.get(term.toLowerCase()));
            }
        }
        return docsWitchHasMinusWords;
    }


}
