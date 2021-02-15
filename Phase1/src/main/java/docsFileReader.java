import java.util.List;

public interface docsFileReader {
    //takes hashedInvertedIndex to have a HashedInvertedIndex to operate on the tokens
    List<DocsWordOccurrence> readFiles(String folderName);
}
