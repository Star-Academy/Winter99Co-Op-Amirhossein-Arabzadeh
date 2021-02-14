import org.junit.*;
import org.mockito.Mock;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;

public class HashInvertedIndexTest {

    static InvertedIndexController myController;
    static InvertedIndex hashedInvertedIndex;
    @BeforeClass
    public static void setUp() {
        myController = new InvertedIndexController();
        myController.processDocs("EnglishData");

        List<String> unsignedWords = new ArrayList<>();
        unsignedWords.add("amirhossein");
        List<String> plusSignedWords = new ArrayList<>();
        plusSignedWords.add("arabzadeh");
        List<String> minusSignedWords = new ArrayList<>();
        minusSignedWords.add("last");
        hashedInvertedIndex = new HashInvertedIndex(unsignedWords, plusSignedWords, minusSignedWords);
    }


    @Test
    public void prepareResultSet() {
        List<String> result = new ArrayList<>();
        //result.add("amir");
        Assert.assertEquals(result, hashedInvertedIndex.prepareResultSet());
    }

}