import java.util.HashMap;
import java.util.List;
import java.util.Set;

public interface Controller {
    List<String> getResult(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);
    //HashMap<String, List<String>> getInvertedIndexTable();
    void processDocs();

}
