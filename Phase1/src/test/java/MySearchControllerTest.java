import org.junit.Assert;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.*;

public class MySearchControllerTest {

    @Test
    public void searchDocs() {
        MyIndexController myIndexController = new MyIndexController();
        myIndexController.processDocs("amir");
        SearchController searchController = new MySearchController();
        List<String> expectedResult = new ArrayList<>();
        expectedResult.add("last");

        List<String> plusSignedWords = new ArrayList<>();
        plusSignedWords.add("amirhossein");
        List<String> unSignedWords = new ArrayList<>();
        unSignedWords.add("arabzadeh");
        List<String> minusSignedWords = new ArrayList<>();
        minusSignedWords.add("last");
        Assert.assertEquals(expectedResult, searchController.searchDocs(plusSignedWords, minusSignedWords, unSignedWords));
    }
}