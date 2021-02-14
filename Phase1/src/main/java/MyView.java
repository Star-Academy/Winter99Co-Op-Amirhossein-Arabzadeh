import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class MyView implements View {
    private List<String> plusSignedInputWords = new ArrayList<>();
    private List<String> minusSignedInputWords = new ArrayList<>();
    private List<String> unSignedInputWords = new ArrayList<>();

    private InputGetter myInputGetter;
    private Partitioner threePartitioner;
    private Controller controller;

    public MyView(InputGetter myInputGetter, Partitioner threePartitioner, Controller controller) {
        this.myInputGetter = myInputGetter;
        this.threePartitioner = threePartitioner;
        this.controller = controller;
    }

    public void run() {
        String userInput = myInputGetter.getInput();
        threePartitioner.partitionInputs(userInput, plusSignedInputWords, minusSignedInputWords, unSignedInputWords);

        List<String> result = controller.getResult(plusSignedInputWords, minusSignedInputWords, unSignedInputWords);
        System.out.println(result);
    }
}
