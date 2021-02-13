import java.io.File;
import java.util.List;
import java.util.Scanner;

public interface Tokenizer {
    List<MyToken>tokenizeOneDoc(File dir, String fileName);
}
