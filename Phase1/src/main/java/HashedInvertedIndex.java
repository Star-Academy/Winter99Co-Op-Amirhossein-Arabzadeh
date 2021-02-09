import java.io.IOException;
import java.util.*;

public class HashedInvertedIndex implements InvertedIndex{

    private List<MyToken> myTokens = new ArrayList<>();

    private ArrayList<String> result = new ArrayList<>();

    private HashMap<String, ArrayList<String>> table = new HashMap<>();

    private ArrayList<String> unSignedWords = new ArrayList<>();
    private ArrayList<String> plusSignedWords = new ArrayList<>();
    private ArrayList<String> minusSignedWords = new ArrayList<>();

    public ArrayList<String> getResult() {
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







        ArrayList<String> tempResult;



        initiateResult(this.table);
        tempResult = result;
        result = getNotSignedDocs(table, tempResult);


        result = plusDocs(table);

        result = minusDocs(table);

        return result;

    }

    public void initiateResult(HashMap<String, ArrayList<String>> table) {
        if (table.containsKey(unSignedWords.get(0).toLowerCase())) {
            result.addAll(table.get(unSignedWords.get(0).toLowerCase()));
        }
    }

    public void mergeIdenticalWordsAndCreateHashTableOfWords() {
        for (int i = 0; i < (myTokens.size() -1); i++) {
            //check if two nere tokens are identical and merge un
            if (myTokens.get(i).getTerm().equals(myTokens.get(i+1).getTerm())) {
                ArrayList<String> docs = new ArrayList<>();
                docs.add(myTokens.get(i).getDoc());
                i++;
                while (myTokens.get(i).getTerm().equals(myTokens.get(i+1).getTerm()) && i<(myTokens.size() - 1)) {
                    if (!docs.contains(myTokens.get(i).getDoc())) {
                        docs.add(myTokens.get(i).getDoc());
                    }
                    i++;
                }
                if (!docs.contains(myTokens.get(i).getDoc())) {
                    docs.add(myTokens.get(i).getDoc());
                }
                i++;
                table.put(myTokens.get(i-1).getTerm(), docs);
            }
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

    public ArrayList<String> getNotSignedDocs(HashMap<String, ArrayList<String>> table, ArrayList<String> tempResult) {
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




    public ArrayList<String> plusDocs(HashMap<String, ArrayList<String>> table) {
        Set<String> docsWitchHasPlusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, docsWitchHasPlusWords, plusSignedWords);


        //clean the result of docs which have not at least one of the plus sugned words
        result = andResultSet(docsWitchHasPlusWords);

        return result;
    }

    public ArrayList<String> andResultSet(Set<String> docsWitchHasPlusWords) {
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

    public ArrayList<String> minusDocs(HashMap<String, ArrayList<String>> table) {

        Set<String> docsWitchHasMinusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, docsWitchHasMinusWords, minusSignedWords);
        result = minusResultSet(docsWitchHasMinusWords);
        return result;

    }

    public ArrayList<String> minusResultSet(Set<String> anotherSet) {
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

    public void createSetOfDifferentModeledInputs(HashMap<String, ArrayList<String>> table, Set<String> docsWitchHasMinusWords, ArrayList<String> partition) {
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
