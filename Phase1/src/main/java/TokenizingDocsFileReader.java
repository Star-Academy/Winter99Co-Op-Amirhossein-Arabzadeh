import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class TokenizingDocsFileReader implements DocsFileReader {
    List<DocsWordOccurrence> tokens = new ArrayList<>();
    public List<DocsWordOccurrence> readFiles(String folderName, Tokenizer lineByLineTokenizer) {

        String path = new File(folderName).getAbsolutePath();
        File dir = new File(path);
        String[] fileNames = dir.list();

        for (String fileName : fileNames) {
            tokens.addAll(lineByLineTokenizer.tokenizeOneDoc(dir, fileName));
        }

        return tokens;

    }

}
