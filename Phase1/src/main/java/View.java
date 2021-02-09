import java.util.ArrayList;
import java.util.Scanner;

public class View {
    public static void main(String[] args) {
        getInput();
        ArrayList<String> result = HashedInvertedIndex.getResult();
        System.out.println(result);
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
                HashedInvertedIndex.setPlusSignedWords(term.substring(1));

            }
            else if (term.startsWith("-")) {
                HashedInvertedIndex.setMinusSignedWords(term.substring(1));
            }
            else {
                HashedInvertedIndex.addTUnSignedWords(term);
            }
        }
    }

}
