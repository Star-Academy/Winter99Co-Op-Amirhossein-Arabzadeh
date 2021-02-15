import java.util.*;

public interface InvertedIndex {

    //produces the result ArrayList for the search action
    List<String> prepareResultSet(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);

}

