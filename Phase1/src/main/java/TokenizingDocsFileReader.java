import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class TokenizingDocsFileReader implements docsFileReader {
    List<DocsWordOccurrence> tokens = new ArrayList<>();
    public List<DocsWordOccurrence> readFiles(String folderName) {

        String path = new File(folderName).getAbsolutePath();
        File dir = new File(path);
        String[] fileNames = dir.list();

        Tokenizer lineByLineTokenizer = new LineByLineTokenizer();
        for (String fileName : fileNames) {
            tokens = lineByLineTokenizer.tokenizeOneDoc(dir, fileName);
        }

        return tokens;

    }

}
