import org.junit.Assert;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;

public class MyIndexControllerTest {

    @Test
    public void processDocs() {
        MyIndexController myIndexController = new MyIndexController();
        myIndexController.processDocs("amir");
        Map<String, List<String>> expectedTable = new HashMap<>();
        List<String> amirHosseinDocs = new ArrayList<>();
        amirHosseinDocs.add("amir");
        expectedTable.put("amirhossein", amirHosseinDocs);
        List<String> arabzadehDocs = new ArrayList<>();
        arabzadehDocs.add("amir");
        expectedTable.put("arabzadeh", arabzadehDocs);
        List<String> lastDocs = new ArrayList<>();
        lastDocs.add("amir");
        lastDocs.add("last");
        expectedTable.put("last", lastDocs);
        for (String word: expectedTable.keySet()) {
            Assert.assertEquals(expectedTable.get(word), myIndexController.getInvertedIndexTable().get(word));
        }
        for (String word: myIndexController.getInvertedIndexTable().keySet()) {
            Assert.assertEquals(expectedTable.get(word), myIndexController.getInvertedIndexTable().get(word));
        }


    }
}