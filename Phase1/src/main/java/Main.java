import java.util.Scanner;

public class Main {
    private static Scanner scanner = new Scanner(System.in);
    public static void main(String[] args) {
        InvertedIndexController myController = new InvertedIndexController();
        myController.processDocs("EnglishData");

        InputGetter inputGetter = new MyInputGetter(scanner);
        Partitioner partitioner = new ThreePartitioner();
        View view = new MyView(inputGetter, partitioner, myController);
        view.run();
    }
}
