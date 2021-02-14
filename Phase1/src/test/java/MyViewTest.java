import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import org.mockito.Mock;

import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.*;
import static org.mockito.Mockito.*;

public class MyViewTest {

    @BeforeClass
    public static void setUp() throws Exception {

    }

    @Test
    public void run() {

        InputGetter myInputGetter = mock(InputGetter.class);
        Partitioner threePartitioner = mock(Partitioner.class);
        Controller myController = mock(Controller.class);
        when(myInputGetter.getInput()).thenReturn("book");
        doNothing().when(threePartitioner).partitionInputs(anyString(), anyList(), anyList(), anyList());
        when(myController.getResult(anyList(), anyList(), anyList())).thenReturn(new ArrayList());
        View myView = new MyView(myInputGetter, threePartitioner, myController);
        myView.run();
        verify(myController).getResult(anyList(), anyList(), anyList());
    }

}