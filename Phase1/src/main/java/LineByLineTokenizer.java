import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class LineByLineTokenizer implements Tokenizer {


    public List<DocsWordOccurrence> tokenizeOneDoc(File dir, String fileName) {
        List<DocsWordOccurrence> tokens = new ArrayList<>();
        Scanner scanner = getScannerForFile(dir, fileName);
        tokenizeLineByLine(fileName, scanner, tokens);
        scanner.close();
        return tokens;
    }

    private Scanner getScannerForFile(File dir, String fileName) {
        File file = new File(dir, fileName);

        Scanner scanner = null;
        try {
            scanner = new Scanner(file);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        return scanner;
    }

    private void tokenizeLineByLine(String fileName, Scanner scanner, List<DocsWordOccurrence> tokens) {

        while (scanner.hasNextLine()) {

            String line = scanner.nextLine();

            for (String word : line.split("\\W+")) {
                addNewToken(fileName, word, tokens);
            }

        }

    }

    private void addNewToken(String fileName, String word, List<DocsWordOccurrence> tokens) {
        DocsWordOccurrence myToken = new DocsWordOccurrence(word.toLowerCase(), fileName);
        tokens.add(myToken);
    }

}
