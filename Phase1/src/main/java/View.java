import java.util.ArrayList;
import java.util.Scanner;

public class View {
    public static void main(String[] args) {
        getInput();
        ArrayList<String> result = InvertedIndex.getResult();
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
                InvertedIndex.setPlusSignedWords(term.substring(1));
                continue;
            }
            if (term.startsWith("-")) {
                InvertedIndex.setMinusSignedWords(term.substring(1));
                continue;
            }
            else {
                InvertedIndex.addTUnSignedWords(term);
            }
        }
    }

}
