import java.util.HashMap;
import java.util.List;

public interface Merger {
    HashMap<String, List<String>> createHashTableOfWords(List<MyToken> tokens);
}
