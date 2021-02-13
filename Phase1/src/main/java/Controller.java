import java.util.List;
import java.util.Set;

public interface Controller {
    //merges tokenized words from the docs content
    void mergeIdenticalWordsAndCreateHashTableOfWords();

    //tokenizes words in docs contents
    List<MyToken> tokenizeContentsOfDocs();
    //adds tokens to myTokens Arraylist witch contains tokens of the words of the contents of a doc
    void addToken(MyToken myToken);

    List<String> getResult(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);
}
