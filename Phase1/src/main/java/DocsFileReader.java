import java.util.List;

public interface DocsFileReader {
    List<DocsWordOccurrence> readFiles(String folderName, Tokenizer lineByLineTokenizer);
}
