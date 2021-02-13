import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class TokenizingMyFileReader implements MyFileReader{
    List<MyToken> tokens = new ArrayList<>();
    public List<MyToken> readFiles() {

        String path = new File("EnglishData").getAbsolutePath();
        File dir = new File(path);
        String[] fileNames = dir.list();

        Tokenizer lineByLineTokenizer = new LineByLineTokenizer();
        for (String fileName : fileNames) {
            tokens = lineByLineTokenizer.tokenizeOneDoc(dir, fileName);
        }

        return tokens;

    }

}
