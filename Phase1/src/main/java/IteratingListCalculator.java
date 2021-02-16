import java.util.*;

public class IteratingListCalculator implements ListCalculator {
    public List<String> minusResultSet(Set<String> anotherSet, List<String> result) {
        ArrayList<String> tempResult;
        tempResult = new ArrayList<>(result);
        for (String term : result) {
            if (anotherSet.contains(term)) {
                tempResult.remove(term);
            }
        }
        return tempResult;
    }

    public Set<String> createSetOfDifferentModeledInputs(List<String> partition, Map<String, List<String>> table) {
        Set<String> docsContainingPartitionWords = new HashSet<>();
        for (String term : partition) {
            if (table.containsKey(term.toLowerCase())) {
                docsContainingPartitionWords.addAll(table.get(term.toLowerCase()));
            }
        }
        return docsContainingPartitionWords;
    }
    public List<String> andResultSet(Set<String> docs, List<String> result) {
        ArrayList<String> tempResult = new ArrayList<>(result);
        for (String term : result) {
            if (!docs.contains(term)) {
                tempResult.remove(term);
            }
        }
        return tempResult;
    }
}
