import java.io.File;
import java.io.IOException;
import java.util.Scanner;

public class MyFileReader {
    public static void readFiles() throws IOException {
        File dir = new File("C:\\Users\\Amirhossein\\Desktop\\EnglishData");

        String[] fileNames = dir.list();

        for (String fileName : fileNames) {
            tokenizeOneDoc(dir, fileName);
        }

    }

    private static void tokenizeOneDoc(File dir, String fileName) throws IOException {
        File file = new File(dir, fileName);

        Scanner scanner = new Scanner(file);

        tokenizeLineByLine(fileName, scanner);
    }

    private static void tokenizeLineByLine(String fileName, Scanner scanner) {

        while (scanner.hasNextLine()) {

            String line = scanner.nextLine();

            for (String word : line.split("\\W+")) {
                addNewToken(fileName, word);
            }

        }

    }

    private static void addNewToken(String fileName, String word) {
        Token token = createToken(fileName, word);
        InvertedIndex.addToken(token);
    }


    private static Token createToken(String fileName, String word) {
        Token token = new Token(word.toLowerCase());
        token.setDoc(fileName);
        return token;
    }

}
