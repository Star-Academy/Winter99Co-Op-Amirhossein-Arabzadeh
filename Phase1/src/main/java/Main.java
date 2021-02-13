public class Main {
    public static void main(String[] args) {
        InvertedIndexController myController = new InvertedIndexController();
        myController.processDocs();
        View view = new MyView();
        view.run();
    }
}
