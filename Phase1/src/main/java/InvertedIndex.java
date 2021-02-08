import java.io.IOException;
import java.util.*;

public class InvertedIndex {

    private static List<Token> tokens = new ArrayList<>();

    private static HashMap<String, ArrayList<String>> table = new HashMap<>();

    private static ArrayList<String> unSignedWords = new ArrayList<>();
    private static ArrayList<String> plusSignedWords = new ArrayList<>();
    private static ArrayList<String> minusSignedWords = new ArrayList<>();

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


        ArrayList<String> result = new ArrayList<>();
        ArrayList<String> tempResult = new ArrayList<>();



        initiateResult(table, result);
        tempResult = result;
        result = getNotSignedDocs(table, searchingTerm, result, tempResult);


        result = plusDocs(table, searchingTerm, result);
        System.out.println(result);

        result = minusDocs(table, searchingTerm, result);

        System.out.println(result);

    }

    private static void initiateResult(HashMap<String, ArrayList<String>> table, ArrayList<String> result) {
        if (table.containsKey(unSignedWords.get(0).toLowerCase())) {
            result.addAll(table.get(unSignedWords.get(0).toLowerCase()));
        }
    }

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

    private static String getInput() {
        Scanner scanner = new Scanner(System.in);
        String searchingTerm = scanner.nextLine();
        partitionInputs(searchingTerm);
        return searchingTerm;
    }

    private static void partitionInputs(String searchingTerm) {
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("+")) {
                plusSignedWords.add(term.substring(1));
                System.out.println("plus isgned word: " + term);
                continue;
            }
            if (term.startsWith("-")) {
                minusSignedWords.add(term.substring(1));
                continue;
            }
            else {
                unSignedWords.add(term);
            }
        }
    }

    private static ArrayList<String> plusDocs(HashMap<String, ArrayList<String>> table, String searchingTerm, ArrayList<String> result) {
        Set<String> docsWitchHasPlusWords = new HashSet<>();
        createSetOfDifferentModeledInputs(table, searchingTerm, docsWitchHasPlusWords, plusSignedWords);


        //clean the result of docs which have not at least one of the plus sugned words
        result = andResultSet(result, docsWitchHasPlusWords);
//        System.out.println("docsWitchHasPlusWords " + plusSignedWords);

        return result;
    }

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
        createSetOfDifferentModeledInputs(table, searchingTerms, docsWitchHasMinusWords, minusSignedWords);
        result = minusResultSet(result, docsWitchHasMinusWords);
        return result;

    }

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

    private static void createSetOfDifferentModeledInputs(HashMap<String, ArrayList<String>> table, String searchingTerms, Set<String> docsWitchHasMinusWords, ArrayList<String> partition) {
        for (String term : partition) {
            if (table.containsKey(term.toLowerCase())) {
                docsWitchHasMinusWords.addAll(table.get(term.toLowerCase()));
            }
        }
    }


    public static void addToken(Token token) {
        tokens.add(token);
    }

}
