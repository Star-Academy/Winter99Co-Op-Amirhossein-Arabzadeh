import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class LineByLineTokenizer implements Tokenizer {

    private List<MyToken> tokens = new ArrayList<>();

    public List<MyToken> tokenizeOneDoc(File dir, String fileName) {
        File file = new File(dir, fileName);

        Scanner scanner = null;
        try {
            scanner = new Scanner(file);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

        tokenizeLineByLine(fileName, scanner);
        return tokens;
    }

    private void tokenizeLineByLine(String fileName, Scanner scanner) {

        while (scanner.hasNextLine()) {

            String line = scanner.nextLine();

            for (String word : line.split("\\W+")) {
                addNewToken(fileName, word);
            }

        }

    }

    private void addNewToken(String fileName, String word) {
        MyToken myToken = new MyToken(word.toLowerCase(), fileName);
        tokens.add(myToken);
    }

}
