import java.util.HashMap;
import java.util.List;

public interface IndexController {
    void processDocs(String folderName);
    HashMap<String, List<String>> getInvertedIndexTable();
}
