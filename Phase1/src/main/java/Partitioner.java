import java.util.List;

public interface Partitioner {
    void partitionInputs(String searchingTerm, List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);
}
