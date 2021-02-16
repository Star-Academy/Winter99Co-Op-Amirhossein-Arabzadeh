import java.util.*;

public class MySearchController implements SearchController {
    private InvertedIndex invertedIndex;


    @Override
    public List<String> searchDocs(List<String> plusSignedInputWords, List<String> minusSignedInputWords, List<String> unSignedInputWords) {
        ListCalculator listCalculator = new IteratingListCalculator();
        IndexController indexController = new MyIndexController();
        ListOperator listOperator = new ArrayListOperator(listCalculator, indexController);
        invertedIndex = new HashInvertedIndex(indexController);
        return invertedIndex.prepareResultSet(plusSignedInputWords, minusSignedInputWords, unSignedInputWords, listOperator);
    }



}
