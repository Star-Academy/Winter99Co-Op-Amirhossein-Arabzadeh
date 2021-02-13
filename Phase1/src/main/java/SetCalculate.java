import java.util.List;
import java.util.Map;
import java.util.Set;

public interface SetCalculate {
    Set<String> createSetOfDifferentModeledInputs(List<String> partition, Map<String, List<String>> table);
    List<String> minusResultSet(Set<String> anotherSet, List<String> result);
    List<String> andResultSet(Set<String> docs, List<String> result);
}
