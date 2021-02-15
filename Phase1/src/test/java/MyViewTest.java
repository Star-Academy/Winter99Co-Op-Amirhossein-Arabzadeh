import org.junit.BeforeClass;
import org.junit.Test;

import java.util.ArrayList;

import static org.mockito.Mockito.*;

public class MyViewTest {

    @BeforeClass
    public static void setUp() throws Exception {

    }

    @Test
    public void run() {

        InputGetter myInputGetter = mock(InputGetter.class);
        Partitioner threePartitioner = mock(Partitioner.class);
        SearchController mySearchController = mock(SearchController.class);
        when(myInputGetter.getInput()).thenReturn("book");
        doNothing().when(threePartitioner).partitionInputs(anyString(), anyList(), anyList(), anyList());
        when(mySearchController.searchDocs(anyList(), anyList(), anyList())).thenReturn(new ArrayList());
        View myView = new MyView(myInputGetter, threePartitioner, mySearchController);
        myView.run();
        verify(mySearchController).searchDocs(anyList(), anyList(), anyList());
    }

}