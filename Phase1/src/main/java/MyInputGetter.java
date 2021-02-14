import java.util.Scanner;

public class MyInputGetter implements InputGetter{
    Scanner scanner;

    public MyInputGetter(Scanner scanner) {
        this.scanner = scanner;
    }

    public String getInput() {
        String searchingTerm = scanner.nextLine();
        return searchingTerm;
    }
}
