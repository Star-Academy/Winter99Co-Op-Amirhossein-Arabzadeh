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
        hashedInvertedIndex = new HashInvertedIndex(myController);
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
        result.add("amir");
        ListCalculator listCalculator = new IteratingListCalculator();
        ListOperator listOperator = new ArrayListOperator(listCalculator, myController);
        Assert.assertEquals(result, hashedInvertedIndex.prepareResultSet(plusSignedWords, minusSignedWords, unsignedWords, listOperator));
    }

}