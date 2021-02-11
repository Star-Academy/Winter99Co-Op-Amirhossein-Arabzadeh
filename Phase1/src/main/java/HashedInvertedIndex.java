import java.io.IOException;
import java.util.*;

public class HashedInvertedIndex implements InvertedIndex{

    private List<MyToken> myTokens = new ArrayList<>();

    private List<String> result = new ArrayList<>();

    private HashMap<String, List<String>> table = new HashMap<>();

    private List<String> unSignedWords = new ArrayList<>();
    private List<String> plusSignedWords = new ArrayList<>();
    private List<String> minusSignedWords = new ArrayList<>();

    public List<String> getResult() {
        tokenizeContentsOfDocs();

        Collections.sort(this.myTokens);


        //iterate the tokensArray to find the identical words to merge them
        mergeIdenticalWordsAndCreateHashTableOfWords();


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

    public void tokenizeContentsOfDocs() {
        MyFileReader tokenizedMyFileReader = new TokenizerMyFileReader();
        try {
            tokenizedMyFileReader.readFiles(this);
        } catch (IOException e) {
            e.printStackTrace();
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
        Set<String> docsWitchHasPlusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, docsWitchHasPlusWords, plusSignedWords);


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

        Set<String> docsWitchHasMinusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, docsWitchHasMinusWords, minusSignedWords);
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

    public void createSetOfDifferentModeledInputs(HashMap<String, List<String>> table, Set<String> docsWitchHasMinusWords, List<String> partition) {
        for (String term : partition) {
            if (table.containsKey(term.toLowerCase())) {
                docsWitchHasMinusWords.addAll(table.get(term.toLowerCase()));
            }
        }
    }


    public void addToken(MyToken myToken) {
        myTokens.add(myToken);
    }

    public void addToUnSignedWords(String unSignedWord) {
        this.unSignedWords.add(unSignedWord);
    }

    public void addToPlusSignedWords(String plusSignedWord) {
        this.plusSignedWords.add(plusSignedWord);
    }

    public void addToMinusSignedWords(String minusSignedWord) {
        this.minusSignedWords.add(minusSignedWord);
    }


}
