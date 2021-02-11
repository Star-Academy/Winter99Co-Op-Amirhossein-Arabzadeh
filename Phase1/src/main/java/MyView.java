import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class MyView {

    public static void main(String[] args) {
        InvertedIndex hashedInvertedIndex = new HashedInvertedIndex();
        getInput(hashedInvertedIndex);
        List<String> result = hashedInvertedIndex.getResult();
        System.out.println(result);
    }

    private static String getInput(InvertedIndex hashedInvertedIndex) {
        Scanner scanner = new Scanner(System.in);
        String searchingTerm = scanner.nextLine();
        partitionInputs(searchingTerm, hashedInvertedIndex);
        return searchingTerm;
    }

    private static void partitionInputs(String searchingTerm, InvertedIndex hashedInvertedIndex) {
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("+")) {
                hashedInvertedIndex.addToPlusSignedWords(term.substring(1));

            }
            else if (term.startsWith("-")) {
                hashedInvertedIndex.addToMinusSignedWords(term.substring(1));
            }
            else {
                hashedInvertedIndex.addToUnSignedWords(term);
            }
        }
    }

}
