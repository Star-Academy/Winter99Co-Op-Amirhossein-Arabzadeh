import java.io.File;
import java.util.List;

public interface Tokenizer {
    List<DocsWordOccurrence>tokenizeOneDoc(File dir, String fileName);
}
