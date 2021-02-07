import java.io.IOException;
import java.util.*;

public class InvertedIndex {

    private static List<Token> tokens = new ArrayList<>();
    public static void main(String[] args) {
        // read files and initiate tokens which may have tokens which may need to be merged
        try {
            MyFileReader.readFiles();
        } catch (IOException e) {
            e.printStackTrace();
        }


        //sort the tokens by their term
        Collections.sort(tokens);
        //create a hashmap to reach search in O(1)
        HashMap<String, ArrayList<String>> table = new HashMap<>();

        //iterate the tokensArray to find the identical words to merge them
        for (int i=0; i < (tokens.size() -1); i++) {
            //check if two neare tokens are identical and merge un
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



        //one word search
//        Scanner scanner = new Scanner(System.in);
//        String searchingTerm = scanner.nextLine();
//        if (table.get(searchingTerm.toLowerCase()).size() != 0) {
//            for (String doc : table.get(searchingTerm.toLowerCase())) {
//                System.out.println(doc);
//            }
//        }


        //get input
        Scanner scanner = new Scanner(System.in);
        String searchingTerm = scanner.nextLine();


        ArrayList<String> result = null;
        ArrayList<String> tempResult = null;

        //get the result for not signed words
        for (String term : searchingTerm.split("\\s")) {
            if (!term.startsWith("+") && !term.startsWith("-")) {
                //if result is not initiated
                if (result == null) {
                    result = new ArrayList<>();
                    if (table.get(term.toLowerCase()) != null) {
                        result.addAll(table.get(term.toLowerCase()));
                    }
                    tempResult = new ArrayList<>(result);
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

        //get set of the or of plus signed words
        Set<String> res2 = new HashSet<>();
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("+")) {
                res2.addAll(table.get(term.substring(1).toLowerCase()));
            }
        }

        if (result == null) {
            result = new ArrayList<>(res2);
        }
        //clean the result of docs which have not at least one of the plus sugned words
        else {
            tempResult = new ArrayList<>(result);
            for (String term : result) {
                if (!res2.contains(term)) {
                    tempResult.remove(term);
                }
            }
            result = tempResult;
        }

        //just like plus signed words
        Set<String> res3 = new HashSet<>();
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("-")) {
                res3.addAll(table.get(term.substring(1).toLowerCase()));
            }
        }

        tempResult = new ArrayList<>(result);
        for (String term : result) {
            if (res3.contains(term)) {
                tempResult.remove(term);
            }
        }
        result = tempResult;
        System.out.println(result);


    }



    public static void addTokens(Token token) {
        tokens.add(token);
    }

}
