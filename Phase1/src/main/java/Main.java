public class Main {
    public static void main(String[] args) {
        ControllerImpl myController = new ControllerImpl();
        myController.processDocs();
        View view = new MyView();
        view.run();
    }
}
