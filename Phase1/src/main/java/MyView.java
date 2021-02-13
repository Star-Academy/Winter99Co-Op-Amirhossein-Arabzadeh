import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class MyView implements View {
    static List<String> plusSignedInputWords = new ArrayList<>();
    static List<String> minusSignedInputWords = new ArrayList<>();
    static List<String> unSignedInputWords = new ArrayList<>();

    public void run() {
        String userInput = getInput();
        partitionInputs(userInput);
        Controller controller = new InvertedIndexController();
        List<String> result = controller.getResult(plusSignedInputWords, minusSignedInputWords, unSignedInputWords);
        System.out.println(result);
    }

    private static String getInput() {
        Scanner scanner = new Scanner(System.in);
        String searchingTerm = scanner.nextLine();
        return searchingTerm;
    }

    private static void partitionInputs(String searchingTerm) {
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("+")) {
                String plusSignedWord = term.substring(1).toLowerCase();
                plusSignedInputWords.add(plusSignedWord);
            }
            else if (term.startsWith("-")) {
                String minusSignedWord = term.substring(1).toLowerCase();
                minusSignedInputWords.add(minusSignedWord);
            }
            else {
                unSignedInputWords.add(term.toLowerCase());
            }
        }
    }

}
