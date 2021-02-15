import java.util.HashMap;
import java.util.List;
import java.util.Set;

public interface SearchController {
    List<String> searchDocs(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);

}
