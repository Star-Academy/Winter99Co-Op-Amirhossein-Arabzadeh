import org.junit.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class ArrayListOperatorTest {
    static ListOperator arrayListOperator;

    static IndexController myIndexController;

    @BeforeClass
    public static void setUp() throws Exception {
        arrayListOperator = new ArrayListOperator();
        myIndexController = new MyIndexController();
        myIndexController.processDocs("EnglishData");

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

        Map<String, List<String>> table = MyIndexController.getInvertedIndexTable();

        Assert.assertEquals(result, arrayListOperator.addUnSignedWordsContainingDocsToResult(testResult, unsignedWords, table));
    }
}