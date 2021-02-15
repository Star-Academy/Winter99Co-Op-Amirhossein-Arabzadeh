import org.junit.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class ArrayListOperatorTest {
    static ListOperator arrayListOperator;

    static IndexController myIndexController;

    @BeforeClass
    public static void setUp() {
        myIndexController = new MyIndexController();
        myIndexController.processDocs("EnglishData");
        arrayListOperator = new ArrayListOperator();

    }

    @Test
    public void intersectUnsignedWordsContainingDocs() {
        List<String> unsignedWords = new ArrayList<>();
        unsignedWords.add("amirhossein");
        unsignedWords.add("javad");
        List<String> result = new ArrayList<>();
        result.add("amir3");

        List<String> testResult = new ArrayList<>();
        testResult.add("amir");
        testResult.add("amir2");
        testResult.add("amir3");

        Assert.assertEquals(result, arrayListOperator.intersectUnsignedWordsContainingDocs(testResult, unsignedWords));
    }

    @Test
    public void removeDocsWithoutPlusWords() {
        List<String> plusSignedWords = new ArrayList<>();
        plusSignedWords.add("arabzadeh");
        List<String> result = new ArrayList<>();
        result.add("amir");

        List<String> testResult = new ArrayList<>();
        testResult.add("amir");
        testResult.add("amir2");
        testResult.add("amir3");

        Assert.assertEquals(result, arrayListOperator.removeDocsWithoutPlusWords(plusSignedWords, testResult));
    }

    @Test
    public void removeMinus() {
        List<String> minusSignedWords = new ArrayList<>();
        minusSignedWords.add("arabzadeh");
        List<String> result = new ArrayList<>();
        result.add("amir2");
        result.add("amir3");

        List<String> testResult = new ArrayList<>();
        testResult.add("amir");
        testResult.add("amir2");
        testResult.add("amir3");
        Assert.assertEquals(result, arrayListOperator.removeDocsContainingMinusSignedWords(minusSignedWords, testResult));
    }
}