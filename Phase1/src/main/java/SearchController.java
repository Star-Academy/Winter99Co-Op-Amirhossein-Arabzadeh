import java.util.List;


public interface SearchController {
    List<String> searchDocs(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);

}
