import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.*;

import static org.junit.Assert.*;

public class IteratingListCalculatorTest {

    @Before
    public void setUp(){


    }

    @Test
    public void minusResultSet() {
        IteratingListCalculator iteratingListCalculator = new IteratingListCalculator();
        List<String> result = new ArrayList<>();
        result.add("ali");
        result.add("hasan");
        result.add("hossein");

        Set<String> anotherSet = new HashSet<>();
        anotherSet.add("hasan");

        List<String> expected = new ArrayList<>();
        expected.add("ali");
        expected.add("hossein");

        Assert.assertEquals(expected, iteratingListCalculator.minusResultSet(anotherSet, result));
    }

    @Test
    public void createSetOfDifferentModeledInputs() {
        IteratingListCalculator iteratingListCalculator = new IteratingListCalculator();
        List<String> partition = new ArrayList<>();
        partition.add("ali");
        partition.add("hasan");
        partition.add("hossein");

        Map<String, List<String>> table = new HashMap<>();
        List<String> aliList = new ArrayList<>();
        aliList.add("doc1");
        List<String> hosseinList = new ArrayList<>();
        hosseinList.add("doc2");
        hosseinList.add("doc3");

        table.put("ali", aliList);
        table.put("hossein", hosseinList);

        Set<String> expected = new HashSet<>();
        expected.add("doc1");
        expected.add("doc2");
        expected.add("doc3");

        Assert.assertEquals(expected, iteratingListCalculator.createSetOfDifferentModeledInputs(partition, table));
    }

    @Test
    public void andResultSet() {
        IteratingListCalculator iteratingListCalculator = new IteratingListCalculator();
        List<String> result = new ArrayList<>();
        result.add("ali");
        result.add("hasan");
        result.add("hossein");

        Set<String> anotherSet = new HashSet<>();
        anotherSet.add("hasan");

        List<String> expected = new ArrayList<>();
        expected.add("hasan");

        Assert.assertEquals(expected, iteratingListCalculator.andResultSet(anotherSet, result));
    }
}