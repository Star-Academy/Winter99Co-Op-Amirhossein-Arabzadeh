import org.junit.*;

import java.util.ArrayList;
import java.util.List;

public class HashInvertedIndexTest {

    static IndexController myController;
    static InvertedIndex hashedInvertedIndex;
    @BeforeClass
    public static void setUp() {
        myController = new MyIndexController();
        myController.processDocs("EnglishData");


        hashedInvertedIndex = new HashInvertedIndex();
    }


    @Test
    public void prepareResultSet() {
        List<String> unsignedWords = new ArrayList<>();
        unsignedWords.add("amirhossein");
        List<String> plusSignedWords = new ArrayList<>();
        plusSignedWords.add("arabzadeh");
        List<String> minusSignedWords = new ArrayList<>();
        minusSignedWords.add("last");
        List<String> result = new ArrayList<>();
        //result.add("amir");
        Assert.assertEquals(result, hashedInvertedIndex.prepareResultSet(unsignedWords, plusSignedWords, minusSignedWords));
    }

}