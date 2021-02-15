import org.junit.*;

import java.util.ArrayList;
import java.util.List;

import static org.mockito.Mockito.mock;

public class HashInvertedIndexTest {

    static IndexController myController;
    static InvertedIndex hashedInvertedIndex;
    @BeforeClass
    public static void setUp() {
        myController = new MyIndexController();
        myController.processDocs("EnglishData");

        IndexController indexController = mock(IndexController.class);
        hashedInvertedIndex = new HashInvertedIndex(indexController);
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
        ListOperator listOperator = mock(ListOperator.class);
        Assert.assertEquals(result, hashedInvertedIndex.prepareResultSet(unsignedWords, plusSignedWords, minusSignedWords, listOperator));
    }

}