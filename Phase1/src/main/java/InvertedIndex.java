import java.util.*;

public interface InvertedIndex {

    List<String> prepareResultSet(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords,  ListOperator listOperator);

}

