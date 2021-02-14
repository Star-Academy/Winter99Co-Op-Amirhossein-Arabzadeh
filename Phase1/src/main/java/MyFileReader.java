import java.io.File;
import java.io.IOException;
import java.util.List;
import java.util.Scanner;

public interface MyFileReader {
    //takes hashedInvertedIndex to have a HashedInvertedIndex to operate on the tokens
    List<MyToken> readFiles(String folderName);
}
