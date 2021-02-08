import java.io.IOException;
import java.util.*;

public class InvertedIndex {

    private static List<Token> tokens = new ArrayList<>();

    private static HashMap<String, ArrayList<String>> table = new HashMap<>();

    public static void main(String[] args) {
        tokenizeContentsOfDocs();

        Collections.sort(tokens);


        //iterate the tokensArray to find the identical words to merge them
        createHashTableOfWords();


        //one word search
//        Scanner scanner = new Scanner(System.in);
//        String searchingTerm = scanner.nextLine();
//        if (table.get(searchingTerm.toLowerCase()).size() != 0) {
//            for (String doc : table.get(searchingTerm.toLowerCase())) {
//                System.out.println(doc);
//            }
//        }


        //get input
        String searchingTerm = getInput();


        ArrayList<String> result = null;
        ArrayList<String> tempResult = null;

//        result = initiateResult(table, searchingTerm, result);

        result = getNotSignedDocs(table, searchingTerm, result, tempResult);

        result = plusDocs(table, searchingTerm, result);

        result = minusDocs(table, searchingTerm, result);

        System.out.println(result);

    }

//    private static ArrayList<String> initiateResult(HashMap<String, ArrayList<String>> table, String searchingTerm, ArrayList<String> result) {
//
//    }

    private static void createHashTableOfWords() {
        for (int i=0; i < (tokens.size() -1); i++) {
            //check if two nere tokens are identical and merge un
            if (tokens.get(i).getTerm().equals(tokens.get(i+1).getTerm())) {
                ArrayList<String> docs = new ArrayList<>();
                docs.add(tokens.get(i).getDoc());
                i++;
                while (tokens.get(i).getTerm().equals(tokens.get(i+1).getTerm()) && i<(tokens.size() - 1)) {
                    if (!docs.contains(tokens.get(i).getDoc())) {
                        docs.add(tokens.get(i).getDoc());
                    }
                    i++;
                }
                if (!docs.contains(tokens.get(i).getDoc())) {
                    docs.add(tokens.get(i).getDoc());
                }
                i++;
                table.put(tokens.get(i-1).getTerm(), docs);
            }
        }
    }

    private static void tokenizeContentsOfDocs() {
        try {
            MyFileReader.readFiles();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static ArrayList<String> getNotSignedDocs(HashMap<String, ArrayList<String>> table, String searchingTerm, ArrayList<String> result, ArrayList<String> tempResult) {
        for (String term : searchingTerm.split("\\s")) {
            if (!term.startsWith("+") && !term.startsWith("-")) {
                //if result is not initiated
                if (result == null) {

                }
                else {
                    for (String doc : result) {
                        //the result has wanted words plus words that are not wanted so we should & it by the set for
                        // the other unsigned word
                        if (!table.get(term.toLowerCase()).contains(doc)) {
                            tempResult.remove(doc);
                        }
                    }
                }
            }
        }
        //set the new array after process to the result array
        result = tempResult;
        return result;
    }

    private static String getInput() {
        Scanner scanner = new Scanner(System.in);
        String searchingTerm = scanner.nextLine();
        return searchingTerm;
    }

    //@org.jetbrains.annotations.NotNull
    private static ArrayList<String> plusDocs(HashMap<String, ArrayList<String>> table, String searchingTerm, ArrayList<String> result) {
        //ArrayList<String> tempResult;
        Set<String> docsWitchHasPlusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, searchingTerm, docsWitchHasPlusWords, "+");

        if (result == null) {
            result = new ArrayList<>(docsWitchHasPlusWords);
        }
        //clean the result of docs which have not at least one of the plus sugned words
        else {
            result = andResultSet(result, docsWitchHasPlusWords);
        }
        return result;
    }

    //@org.jetbrains.annotations.NotNull
    private static ArrayList<String> andResultSet(ArrayList<String> result, Set<String> docsWitchHasPlusWords) {
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

    private static ArrayList<String> minusDocs(HashMap<String, ArrayList<String>> table, String searchingTerms, ArrayList<String> result) {

        Set<String> docsWitchHasMinusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, searchingTerms, docsWitchHasMinusWords, "-");
        result = minusResultSet(result, docsWitchHasMinusWords);
        return result;

    }

    //@org.jetbrains.annotations.NotNull
    private static ArrayList<String> minusResultSet(ArrayList<String> result, Set<String> anotherSet) {
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

    private static void createSetOfDifferentModeledInputs(HashMap<String, ArrayList<String>> table, String searchingTerms, Set<String> docsWitchHasMinusWords, String s) {
        for (String term : searchingTerms.split("\\s")) {
            if (term.startsWith(s)) {
                docsWitchHasMinusWords.addAll(table.get(term.substring(1).toLowerCase()));
            }
        }
    }


    public static void addToken(Token token) {
        tokens.add(token);
    }

}
