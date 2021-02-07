import java.io.IOException;
import java.util.*;

public class InvertedIndex {
    //HashMap<String, Token> hash = new HashMap<>();

    private static List<Token> tokens = new ArrayList<>();
    public static void main(String[] args) {
        // read files and initiate tokens which may have tokens which need to be merged
        try {
            MyFileReader.readFiles();
        } catch (IOException e) {
            e.printStackTrace();
        }



        //String content = MyFileReader.getContent().toString();

        Collections.sort(tokens);
        HashMap<String, ArrayList<String>> table = new HashMap<>();

        for (int i=0; i < (tokens.size() -1); i++) {
            if (tokens.get(i).getTerm().equals(tokens.get(i+1).getTerm())) {
                ArrayList<String> docs = new ArrayList<>();
                docs.add(tokens.get(i).getDoc());
                //tokens.get(i).addToDocs(tokens.get(i+1).getDoc());
                i++;
                while (tokens.get(i).getTerm().equals(tokens.get(i+1).getTerm()) && i<(tokens.size() - 1)) {
                    //tokens.get(i).addToDocs(tokens.get(i+1).getDoc());
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

//        Set<String> terms = new HashSet<>();
//        for (Token token : tokens) {
//            terms.add(token.getTerm());
//        }


        //one word search
//        Scanner scanner = new Scanner(System.in);
//        String searchingTerm = scanner.nextLine();
//        if (table.get(searchingTerm.toLowerCase()).size() != 0) {
//            for (String doc : table.get(searchingTerm.toLowerCase())) {
//                System.out.println(doc);
//            }
//        }
        Scanner scanner = new Scanner(System.in);
        String searchingTerm = scanner.nextLine();
        ArrayList<String> result = null;
        ArrayList<String> tempResult = null;
        for (String term : searchingTerm.split("\\s")) {
            if (!term.startsWith("+") && !term.startsWith("-")) {
                if (result == null) {
                    result = new ArrayList<>();
                    if (table.get(term.toLowerCase()) != null) {
                        result.addAll(table.get(term.toLowerCase()));
                    }
                    tempResult = new ArrayList<>(result);
                }
                else {
                    for (String doc : result) {
                        if (!table.get(term.toLowerCase()).contains(doc)) {
                            tempResult.remove(doc);
                        }
                    }
                }
            }
        }

        result = tempResult;

        Set<String> res2 = new HashSet<>();
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("+")) {
                res2.addAll(table.get(term.substring(1).toLowerCase()));
            }
        }

        System.out.println(result);
        tempResult = new ArrayList<>(result);
        for (String term : result) {
            if (!res2.contains(term)) {
                tempResult.remove(term);
            }
        }
        result = tempResult;


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


    }

    public static void addTokens(Token token) {
        tokens.add(token);
    }

}
