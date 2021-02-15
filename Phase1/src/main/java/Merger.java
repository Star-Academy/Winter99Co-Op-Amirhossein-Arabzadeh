import java.util.HashMap;
import java.util.List;

public interface Merger {
    HashMap<String, List<String>> createHashTableOfWordsFromSortedList(List<DocsWordOccurrence> tokens);
}
