import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class MyView implements View {
    static List<String> plusSignedInputWords = new ArrayList<>();
    static List<String> minusSignedInputWords = new ArrayList<>();
    static List<String> unSignedInputWords = new ArrayList<>();

    public void run() {
        getInput();
        Controller controller = new ControllerImpl();
        List<String> result = controller.getResult(plusSignedInputWords, minusSignedInputWords, unSignedInputWords);
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
                plusSignedInputWords.add(term.substring(1));

            }
            else if (term.startsWith("-")) {
                minusSignedInputWords.add(term.substring(1));
            }
            else {
                unSignedInputWords.add(term);
            }
        }
    }

}
