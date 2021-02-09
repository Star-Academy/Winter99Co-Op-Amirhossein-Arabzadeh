import java.util.ArrayList;
import java.util.Scanner;

public interface View {
    String getInput();

    //departs input words to different ArrayList with attention to signs before the input words
    // takes searchingTerm witch is input line by the user
    void partitionInputs(String searchingTerm);
}
