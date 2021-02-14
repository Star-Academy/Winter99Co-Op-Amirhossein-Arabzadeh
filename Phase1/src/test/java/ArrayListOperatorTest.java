import org.junit.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;

public class ArrayListOperatorTest {
    static ListOperator arrayListOperator;

    static InvertedIndexController myController;

    @BeforeClass
    public static void setUp() throws Exception {
        arrayListOperator = new ArrayListOperator();
        myController = new InvertedIndexController();
        myController.processDocs("EnglishData");

    }

    @Test
    public void addUnSignedWordsContainingDocsToResult() {
        List<String> unsignedWords = new ArrayList<>();
        unsignedWords.add("amirhossein");
        unsignedWords.add("javad");
        List<String> result = new ArrayList<>();
        result.add("amir3");

        List<String> testResult = new ArrayList<>();
        testResult.add("amir");
        testResult.add("amir2");
        testResult.add("amir3");

        Map<String, List<String>> table = InvertedIndexController.getInvertedIndexTable();

        Assert.assertEquals(result, arrayListOperator.addUnSignedWordsContainingDocsToResult(testResult, unsignedWords, table));
    }
}