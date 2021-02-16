import java.util.Scanner;

public class Main {
    private static Scanner scanner = new Scanner(System.in);
    public static void main(String[] args) {
        IndexController myIndexController = new MyIndexController();
        myIndexController.processDocs("EnglishData");

        SearchController searchController = new MySearchController();

        InputGetter inputGetter = new MyInputGetter(scanner);
        Partitioner partitioner = new ThreePartitioner();
        View view = new MyView(inputGetter, partitioner, searchController);
        view.run();
    }
}
