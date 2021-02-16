import org.junit.Assert;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.*;

public class ThreePartitionerTest {

    @Test
    public void partitionInputs() {
        List<String> unsignedWords1 = new ArrayList<>();
        unsignedWords1.add("amirhossein");
        List<String> plusSignedWords2 = new ArrayList<>();
        plusSignedWords2.add("arabzadeh");
        List<String> minusSignedWords3 = new ArrayList<>();
        minusSignedWords3.add("last");

        List<String> unsignedWords = new ArrayList<>();
        List<String> plusSignedWords = new ArrayList<>();
        List<String> minusSignedWords = new ArrayList<>();

        String searchingTerm = "amirhossein +arabzadeh -last";

        Partitioner partitioner = new ThreePartitioner();
        partitioner.partitionInputs(searchingTerm, plusSignedWords, minusSignedWords, unsignedWords);

        Assert.assertEquals(unsignedWords1, unsignedWords);
        Assert.assertEquals(plusSignedWords2, plusSignedWords);
        Assert.assertEquals(minusSignedWords3, minusSignedWords);
    }
}