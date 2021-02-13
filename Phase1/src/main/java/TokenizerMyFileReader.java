import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class TokenizerMyFileReader implements MyFileReader{
    InvertedIndex hashedInvertedIndex;
    List<MyToken> tokens = new ArrayList<>();
    public List<MyToken> readFiles() {

        String path = new File("EnglishData").getAbsolutePath();
        File dir = new File(path);
        this.hashedInvertedIndex = hashedInvertedIndex;
        String[] fileNames = dir.list();


        for (String fileName : fileNames) {
            tokenizeOneDoc(dir, fileName);
        }

        return tokens;

    }

    public void tokenizeOneDoc(File dir, String fileName) {
        File file = new File(dir, fileName);

        Scanner scanner = null;
        try {
            scanner = new Scanner(file);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

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
        tokens.add(myToken);
    }


    public MyToken createToken(String fileName, String word) {
        MyToken myToken = new MyToken(word.toLowerCase());
        myToken.setDoc(fileName);
        return myToken;
    }

}
