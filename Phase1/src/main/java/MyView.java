import java.util.ArrayList;
import java.util.List;

public class MyView implements View {
    private List<String> plusSignedInputWords = new ArrayList<>();
    private List<String> minusSignedInputWords = new ArrayList<>();
    private List<String> unSignedInputWords = new ArrayList<>();

    private InputGetter myInputGetter;
    private Partitioner threePartitioner;
    private SearchController searchController;

    public MyView(InputGetter myInputGetter, Partitioner threePartitioner, SearchController searchController) {
        this.myInputGetter = myInputGetter;
        this.threePartitioner = threePartitioner;
        this.searchController = searchController;
    }

    public void run() {
        String userInput = myInputGetter.getInput();
        threePartitioner.partitionInputs(userInput, plusSignedInputWords, minusSignedInputWords, unSignedInputWords);

        List<String> result = searchController.getSetOfDocsForUser(plusSignedInputWords, minusSignedInputWords, unSignedInputWords);
        System.out.println(result);
    }
}
