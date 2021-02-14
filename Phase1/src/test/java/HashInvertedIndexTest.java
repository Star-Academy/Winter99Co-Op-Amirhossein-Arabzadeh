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
        myController.processDocs();

        List<String> unsignedWords = new ArrayList<>();
        unsignedWords.add("Street");
        List<String> plusSignedWords = new ArrayList<>();
        unsignedWords.add("Stories");
        List<String> minusSignedWords = new ArrayList<>();
        unsignedWords.add("last");
        hashedInvertedIndex = new HashInvertedIndex(unsignedWords, plusSignedWords, minusSignedWords);
    }


    @Test
    public void prepareResultSet() {


        ListOperator listOperator = new ArrayListOperator();
        Map<String, List<String>> table = InvertedIndexController.getInvertedIndexTable();

        List<String> result = new ArrayList<>();
        result.add("59286");
        Assert.assertEquals(result, hashedInvertedIndex.prepareResultSet());
    }
}