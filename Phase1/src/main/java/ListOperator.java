import java.util.List;
import java.util.Map;
import java.util.Set;

public interface ListOperator {
    List<String> addUnSignedWordsContainingDocsToResult(List<String> result, List<String> unSignedWords, Map<String, List<String>> table);
    List<String> removeDocsWithoutPlusWords(List<String> plusSignedWords, List<String> result, Map<String, List<String>> table);
    List<String> removeMinus(List<String> minusSignedWords, List<String> result, Map<String, List<String>> table);

    }
