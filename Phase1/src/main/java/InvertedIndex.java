import java.util.*;

public interface InvertedIndex {

    //produces the result ArrayList for the search action
    List<String> prepareResult(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords);

    //initiates the result ArrayList for the first unsigned word; it takes the hashTable used for
    //maintain words as key and containing docs list as value
    void initiateResult(HashMap<String, List<String>> table);



    //returns docs with contain unsigned searching words, tempResult is use to solve the problem of
    //concurrent modification on the result ArrayList
    List<String> getNotSignedDocs(HashMap<String, List<String>> table, List<String> tempResult);

    //ands result ArrayList with list of docs containing plus signed words
    List<String> plusDocs(HashMap<String, List<String>> table);

    //ands result ArrayList with set of docs containing plusSigned words
    List<String> andResultSet(Set<String> docsWitchHasPlusWords);

    //look likes plusDocs()
    List<String> minusDocs(HashMap<String, List<String>> table);

    //look likes andResultSet()
    List<String> minusResultSet(Set<String> anotherSet);

    //creates sets of plus/minus signed words
    //uses partition witch is an ArrayList containing words with minus/plus/nothing signed words
    Set<String> createSetOfDifferentModeledInputs(HashMap<String, List<String>> table, List<String> partition);


}

