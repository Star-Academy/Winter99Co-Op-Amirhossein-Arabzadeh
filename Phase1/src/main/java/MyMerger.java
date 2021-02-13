import java.util.*;

public class MyMerger implements Merge{
    public HashMap<String, List<String>> mergeIdenticalWordsAndCreateHashTableOfWords(List<MyToken> tokens) {
        HashMap<String, List<String>> table = new HashMap<>();
        for (int i = 0; i < (tokens.size() -1); i++) {
            Set<String> docs = new HashSet<>();
            while (i<(tokens.size() - 2) && tokens.get(i).getTerm().equals(tokens.get(i+1).getTerm())) {
                docs.add(tokens.get(i).getDoc());
                i++;
            }
            docs.add(tokens.get(i).getDoc());
            table.put(tokens.get(i).getTerm(), new ArrayList<>(docs));
            i++;
        }
        return table;
    }
}
