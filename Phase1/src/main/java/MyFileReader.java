import java.io.File;
import java.io.IOException;
import java.util.List;
import java.util.Scanner;

public interface MyFileReader {
    //takes hashedInvertedIndex to have a HashedInvertedIndex to operate on the tokens
    List<MyToken> readFiles();

    void tokenizeOneDoc(File dir, String fileName) throws IOException;

    void tokenizeLineByLine(String fileName, Scanner scanner);

    //adds token to tokensArray in the HashedInverted obj
    //file name is name of the doc and word is the word witch is in the doc
    void addNewToken(String fileName, String word);


    // file name is name of the doc and word is the word witch is in the doc
    MyToken createToken(String fileName, String word);
}
