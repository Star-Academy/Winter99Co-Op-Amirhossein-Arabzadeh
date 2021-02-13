import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class TokenizerMyFileReader implements MyFileReader{
    List<MyToken> tokens = new ArrayList<>();
    public List<MyToken> readFiles() {

        String path = new File("EnglishData").getAbsolutePath();
        File dir = new File(path);
        String[] fileNames = dir.list();

        Tokenize myTokenizer = new MyTokenizer();
        for (String fileName : fileNames) {
            tokens = myTokenizer.tokenizeOneDoc(dir, fileName);
        }

        return tokens;

    }

}
