import java.util.List;

public class ThreePartitioner implements Partitioner {
    @Override
    public void partitionInputs(String searchingTerm, List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {
        for (String term : searchingTerm.split("\\s")) {
            if (term.startsWith("+")) {
                String plusSignedWord = term.substring(1).toLowerCase();
                plusSignedInputWords.add(plusSignedWord);
            }
            else if (term.startsWith("-")) {
                String minusSignedWord = term.substring(1).toLowerCase();
                minusSignedInputWords.add(minusSignedWord);
            }
            else {
                unSignedInputWords.add(term.toLowerCase());
            }
        }
    }
}
