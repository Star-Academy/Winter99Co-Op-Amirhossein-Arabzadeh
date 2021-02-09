import java.io.File;
import java.io.IOException;
import java.util.Scanner;

public class TokenizerMyFileReader implements MyFileReader{
    InvertedIndex hashedInvertedIndex;
    public void readFiles(InvertedIndex hashedInvertedIndex) throws IOException {
        String path = new File("EnglishData").getAbsolutePath();
        File dir = new File(path);
        this.hashedInvertedIndex = hashedInvertedIndex;
        String[] fileNames = dir.list();

        for (String fileName : fileNames) {
            tokenizeOneDoc(dir, fileName);
        }

    }

    public void tokenizeOneDoc(File dir, String fileName) throws IOException {
        File file = new File(dir, fileName);

        Scanner scanner = new Scanner(file);

        tokenizeLineByLine(fileName, scanner);
    }

    public void tokenizeLineByLine(String fileName, Scanner scanner) {

        while (scanner.hasNextLine()) {

            String line = scanner.nextLine();

            for (String word : line.split("\\W+")) {
                addNewToken(fileName, word);
            }

        }

    }

    public void addNewToken(String fileName, String word) {
        MyToken myToken = createToken(fileName, word);
        hashedInvertedIndex.addToken(myToken);
    }


    public MyToken createToken(String fileName, String word) {
        MyToken myToken = new MyToken(word.toLowerCase());
        myToken.setDoc(fileName);
        return myToken;
    }

}
