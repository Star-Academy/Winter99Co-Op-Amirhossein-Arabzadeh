import java.util.List;
import java.util.Map;
import java.util.Set;

public interface SetOperate {
    List<String> addUnSignedWordsContainingDocsToResult(List<String> result, List<String> unSignedWords, Map<String, List<String>> table);
    List<String> andResultWithSetOfDocsContainingPlusSignedWords(List<String> plusSignedWords, List<String> result, Map<String, List<String>> table);
    List<String> removeMinusSignedContainingDocsFromResult(List<String> minusSignedWords, List<String> result, Map<String, List<String>> table);

    }
